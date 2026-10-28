using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        private const string HomeUrl = "https://www.google.com/";
        private const string AppTitle = "Secure Web Browser";

        private readonly SQLiteHandler database = new SQLiteHandler();

        // Adresele deja verificate (permit Inapoi/Inainte fara a verifica din nou).
        private readonly HashSet<string> approvedUrls =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Copia in memorie a cuvintelor din baza de date.
        private List<string> keywords = new List<string>();
        private int blockedCount;

        public Form1()
        {
            InitializeComponent();
            Logger.Init();
        }

        // ------------------------------------------------------------------
        // Pornire / inchidere
        // ------------------------------------------------------------------

        private async void Form1_Load(object sender, EventArgs e)
        {
            ResizeAddressBar();
            UpdateNavigationButtons();

            // Cuvintele cheie se incarca automat din SQLite la pornire.
            LoadKeywordsFromDatabase(null);

            await Logger.LogAsync("Application started. Keywords loaded: " + keywords.Count);
            await NavigateToAsync(HomeUrl);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0:dd.MM.yyyy HH:mm:ss.fff} - Application closed",
                    DateTime.Now));
            }
            catch (Exception)
            {
            }

            database.Dispose();
            Logger.Shutdown();
        }

        // ------------------------------------------------------------------
        // Cuvinte cheie (SQLite + ComboBox)
        // ------------------------------------------------------------------

        private bool EnsureDatabase()
        {
            if (database.IsConnected)
                return true;

            try
            {
                database.ConnectToDb();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Could not open the keyword database.\n\n" + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Reincarca lista din baza de date si actualizeaza ComboBox-ul.
        /// </summary>
        private void LoadKeywordsFromDatabase(string selectKeyword)
        {
            keywords = EnsureDatabase() ? SafeReadKeywords() : new List<string>();
            approvedUrls.Clear();   // lista s-a schimbat: adresele se verifica din nou

            cmbKeywords.Items.Clear();
            foreach (string keyword in keywords)
                cmbKeywords.Items.Add(keyword);

            lblKeywords.Text = "Blocked keywords (" + keywords.Count + "):";

            if (keywords.Count > 0)
            {
                int index = selectKeyword == null
                    ? 0
                    : keywords.FindIndex(k => string.Equals(k, selectKeyword, StringComparison.OrdinalIgnoreCase));
                cmbKeywords.SelectedIndex = index >= 0 ? index : 0;
            }

            UpdateKeywordButtons();
        }

        private List<string> SafeReadKeywords()
        {
            try
            {
                return database.GetAllKeywords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not read the keywords.\n\n" + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>();
            }
        }

        private void UpdateKeywordButtons()
        {
            bool hasSelection = cmbKeywords.SelectedIndex >= 0;
            btnDeleteKeyword.Enabled = hasSelection;
            miDeleteKeyword.Enabled = hasSelection;
        }

        private void Keywords_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateKeywordButtons();
        }

        private async void AddKeyword_Click(object sender, EventArgs e)
        {
            if (!EnsureDatabase())
                return;

            using (AddKeywordForm form = new AddKeywordForm(keywords))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                string keyword = form.Keyword;
                try
                {
                    database.InsertKeyword(keyword);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "The keyword could not be saved.\n\n" + ex.Message,
                        "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lista se actualizeaza automat dupa introducerea unui cuvant nou.
                LoadKeywordsFromDatabase(keyword);
                SetStatus("Keyword \"" + keyword + "\" added. Websites containing it are now blocked.");
                await Logger.LogAsync("Keyword added: " + keyword);
            }
        }

        private async void DeleteKeyword_Click(object sender, EventArgs e)
        {
            string keyword = cmbKeywords.SelectedItem as string;
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show(this, "First choose the keyword you want to delete from the list.",
                    "No keyword selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult answer = MessageBox.Show(this,
                "Are you sure you want to delete the keyword \"" + keyword + "\"?\n\n" +
                "Websites containing this word will no longer be blocked.",
                "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (answer != DialogResult.Yes)
                return;

            try
            {
                if (!EnsureDatabase())
                    return;
                database.DeleteKeyword(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "The keyword could not be deleted.\n\n" + ex.Message,
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadKeywordsFromDatabase(null);
            SetStatus("Keyword \"" + keyword + "\" deleted.");
            await Logger.LogAsync("Keyword deleted: " + keyword);
        }

        // ------------------------------------------------------------------
        // Navigare + filtrare URL (LINQ + async)
        // ------------------------------------------------------------------

        private static Uri BuildUri(string input)
        {
            string text = input.Trim();
            Uri uri;

            if (text.Contains("://"))
            {
                if (Uri.TryCreate(text, UriKind.Absolute, out uri) &&
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                    return uri;
                return null;
            }

            bool looksLikeAddress = !text.Any(char.IsWhiteSpace) &&
                (text.Contains(".") || text.StartsWith("localhost", StringComparison.OrdinalIgnoreCase));

            string url = looksLikeAddress
                ? "https://" + text
                : "https://www.google.com/search?q=" + Uri.EscapeDataString(text);

            return Uri.TryCreate(url, UriKind.Absolute, out uri) ? uri : null;
        }

        /// <summary>
        /// Filtrare asincrona cu LINQ: returneaza primul cuvant cheie continut in URL (sau null).
        /// </summary>
        private Task<string> FindBlockedKeywordAsync(string url)
        {
            string[] snapshot = keywords.ToArray();
            string decoded = url;
            try { decoded = Uri.UnescapeDataString(url); } catch (Exception) { }

            return Task.Run(() => snapshot.FirstOrDefault(k =>
                url.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0 ||
                decoded.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        private async Task NavigateToAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            Uri uri = BuildUri(input);
            if (uri == null)
            {
                MessageBox.Show(this,
                    "The address you entered is not valid.\nExample of an address: www.wikipedia.org",
                    "Invalid address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CheckAndNavigateAsync(uri);
        }

        private async Task CheckAndNavigateAsync(Uri uri)
        {
            try
            {
                SetStatus("Checking address...");
                await Logger.LogAsync("Navigation requested: " + uri.AbsoluteUri);

                string match = await FindBlockedKeywordAsync(uri.AbsoluteUri);
                if (match != null)
                {
                    blockedCount++;
                    lblBlocked.Text = "Blocked sites: " + blockedCount;
                    ShowBlockedPage(uri, match);
                    await Logger.LogAsync("URL blocked (" + match + "): " + uri.AbsoluteUri);
                    return;
                }

                approvedUrls.Add(uri.AbsoluteUri);
                webBrowser1.Navigate(uri);
            }
            catch (Exception ex)
            {
                SetStatus("Error opening the page: " + ex.Message);
            }
        }

        private void ShowBlockedPage(Uri uri, string keyword)
        {
            string html =
                "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                "<meta http-equiv='X-UA-Compatible' content='IE=edge'><title>Website blocked</title></head>" +
                "<body style='font-family:Segoe UI,Arial,sans-serif;background:#f6f7fb;margin:0;'>" +
                "<div style='max-width:560px;margin:90px auto;background:#fff;border:1px solid #d9dce6;" +
                "border-radius:8px;padding:36px;text-align:center;'>" +
                "<div style='font-size:56px;color:#c0392b;'>&#9940;</div>" +
                "<h1 style='color:#c0392b;font-size:26px;margin:8px 0 14px;'>This website is blocked</h1>" +
                "<p style='font-size:16px;color:#333;'>The address contains the keyword " +
                "<b>&ldquo;" + WebUtility.HtmlEncode(keyword) + "&rdquo;</b>, which is in the list of blocked keywords.</p>" +
                "<p style='font-size:13px;color:#777;word-break:break-all;'>" +
                WebUtility.HtmlEncode(uri.AbsoluteUri) + "</p>" +
                "<p style='font-size:14px;color:#555;'>To allow access, delete the keyword " +
                "from the keyword list (top bar of the application).</p>" +
                "</div></body></html>";

            webBrowser1.DocumentText = html;
            txtAddress.Text = uri.AbsoluteUri;
            SetStatus("Access blocked: keyword \"" + keyword + "\".");
        }

        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url == null)
                return;

            // Paginile interne (about:blank, pagina de blocare) si alte scheme nu se filtreaza.
            if (e.Url.Scheme != Uri.UriSchemeHttp && e.Url.Scheme != Uri.UriSchemeHttps)
                return;

            string url = e.Url.AbsoluteUri;

            // Cadre din pagina (iframe): verificare rapida, fara reincarcare.
            if (!string.IsNullOrEmpty(e.TargetFrameName))
            {
                if (keywords.Any(k => url.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0))
                    e.Cancel = true;
                return;
            }

            if (approvedUrls.Contains(url))
                return;

            // Link apasat in pagina / redirectare: oprim, verificam asincron, apoi navigam.
            e.Cancel = true;
            _ = CheckAndNavigateAsync(e.Url);
        }

        private void Browser_NewWindow(object sender, CancelEventArgs e)
        {
            // Linkurile care s-ar deschide in fereastra noua se deschid in aceeasi fereastra.
            e.Cancel = true;
            string href = null;
            try
            {
                HtmlElement element = webBrowser1.Document == null ? null : webBrowser1.Document.ActiveElement;
                if (element != null)
                    href = element.GetAttribute("href");
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrEmpty(href))
                _ = NavigateToAsync(href);
        }

        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url != null && (e.Url.Scheme == Uri.UriSchemeHttp || e.Url.Scheme == Uri.UriSchemeHttps))
                txtAddress.Text = e.Url.AbsoluteUri;
            UpdateNavigationButtons();
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            progressBar.Visible = false;
            UpdateNavigationButtons();
        }

        private void Browser_DocumentTitleChanged(object sender, EventArgs e)
        {
            string title = webBrowser1.DocumentTitle;
            Text = string.IsNullOrWhiteSpace(title) ? AppTitle : title + " - " + AppTitle;
        }

        private void Browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.MaximumProgress > 0 && e.CurrentProgress >= 0 && e.CurrentProgress < e.MaximumProgress)
            {
                progressBar.Maximum = (int)Math.Min(int.MaxValue, e.MaximumProgress);
                progressBar.Value = (int)Math.Min(progressBar.Maximum, e.CurrentProgress);
                progressBar.Visible = true;
            }
            else
            {
                progressBar.Visible = false;
            }
        }

        private void Browser_StatusTextChanged(object sender, EventArgs e)
        {
            string text = webBrowser1.StatusText;
            if (!string.IsNullOrEmpty(text))
                SetStatus(text);
        }

        private void Browser_NavStateChanged(object sender, EventArgs e)
        {
            UpdateNavigationButtons();
        }

        private void UpdateNavigationButtons()
        {
            btnBack.Enabled = miBack.Enabled = webBrowser1.CanGoBack;
            btnForward.Enabled = miForward.Enabled = webBrowser1.CanGoForward;
        }

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        // ------------------------------------------------------------------
        // Butoane / meniuri de navigare
        // ------------------------------------------------------------------

        private async void Back_Click(object sender, EventArgs e)
        {
            await Logger.LogAsync("User clicked Back");
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private async void Forward_Click(object sender, EventArgs e)
        {
            await Logger.LogAsync("User clicked Forward");
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private async void Home_Click(object sender, EventArgs e)
        {
            await Logger.LogAsync("User clicked Home");
            await NavigateToAsync(HomeUrl);
        }

        private async void Refresh_Click(object sender, EventArgs e)
        {
            await Logger.LogAsync("User clicked Reload");
            if (webBrowser1.Url != null && webBrowser1.Url.Scheme != "about")
            {
                // Re-verificam adresa (lista de cuvinte s-ar fi putut schimba).
                await CheckAndNavigateAsync(webBrowser1.Url);
            }
        }

        private async void Stop_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            progressBar.Visible = false;
            SetStatus("Loading stopped.");
            await Logger.LogAsync("User clicked Stop");
        }

        private async void Go_Click(object sender, EventArgs e)
        {
            await Logger.LogAsync("User clicked Go");
            await NavigateToAsync(txtAddress.Text);
        }

        private async void Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await Logger.LogAsync("User pressed Enter in the address bar");
                await NavigateToAsync(txtAddress.Text);
            }
        }

        private void FocusAddress_Click(object sender, EventArgs e)
        {
            txtAddress.Focus();
            txtAddress.SelectAll();
        }

        // Esc = oprirea incarcarii (Escape nu poate fi ShortcutKeys la un meniu).
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Stop_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NavStrip_SizeChanged(object sender, EventArgs e)
        {
            ResizeAddressBar();
        }

        /// <summary>
        /// Bara de adresa ocupa tot spatiul ramas pe bara de navigare.
        /// </summary>
        private void ResizeAddressBar()
        {
            int used = navStrip.Padding.Horizontal + 8;
            foreach (ToolStripItem item in navStrip.Items)
            {
                if (item != txtAddress)
                    used += item.Width + item.Margin.Horizontal;
            }

            txtAddress.Width = Math.Max(150, navStrip.Width - used - txtAddress.Margin.Horizontal);
        }

        // ------------------------------------------------------------------
        // Ajutor
        // ------------------------------------------------------------------

        private void HowTo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                "How to use the application\n\n" +
                "• Type a web address (for example www.wikipedia.org) or search words in the address bar and press Enter.\n" +
                "• The Back, Forward, Home, Reload and Stop buttons work like in any browser.\n" +
                "• Websites whose address contains a word from the keyword list are blocked.\n" +
                "• Click \"+ Add keyword\" to block a new website.\n" +
                "• Choose a keyword from the list and click \"– Delete keyword\" to remove it.",
                "How to use", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                AppTitle + "\nLab 3 + Lab 4\n\n" +
                "Windows Forms, LINQ, async/await, Trace, ADO.NET + SQLite.",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
