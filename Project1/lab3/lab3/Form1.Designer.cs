namespace lab3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.miBack = new System.Windows.Forms.ToolStripMenuItem();
            this.miForward = new System.Windows.Forms.ToolStripMenuItem();
            this.miHome = new System.Windows.Forms.ToolStripMenuItem();
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miStop = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miFocusAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.miKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddKeyword = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteKeyword = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miHowTo = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.navStrip = new System.Windows.Forms.ToolStrip();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.btnHome = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.txtAddress = new System.Windows.Forms.ToolStripTextBox();
            this.btnGo = new System.Windows.Forms.ToolStripButton();
            this.keywordStrip = new System.Windows.Forms.ToolStrip();
            this.lblKeywords = new System.Windows.Forms.ToolStripLabel();
            this.cmbKeywords = new System.Windows.Forms.ToolStripComboBox();
            this.btnAddKeyword = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteKeyword = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblBlocked = new System.Windows.Forms.ToolStripStatusLabel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.navStrip.SuspendLayout();
            this.keywordStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // menuStrip1
            //
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNavigation,
            this.miKeywords,
            this.miHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.TabIndex = 0;
            //
            // miNavigation
            //
            this.miNavigation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBack,
            this.miForward,
            this.miHome,
            this.miRefresh,
            this.miStop,
            this.miSep1,
            this.miFocusAddress});
            this.miNavigation.Name = "miNavigation";
            this.miNavigation.Text = "&Navigation";
            //
            // miBack
            //
            this.miBack.Name = "miBack";
            this.miBack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
            this.miBack.Text = "Back";
            this.miBack.Click += new System.EventHandler(this.Back_Click);
            //
            // miForward
            //
            this.miForward.Name = "miForward";
            this.miForward.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
            this.miForward.Text = "Forward";
            this.miForward.Click += new System.EventHandler(this.Forward_Click);
            //
            // miHome
            //
            this.miHome.Name = "miHome";
            this.miHome.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Home)));
            this.miHome.Text = "Home page";
            this.miHome.Click += new System.EventHandler(this.Home_Click);
            //
            // miRefresh
            //
            this.miRefresh.Name = "miRefresh";
            this.miRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miRefresh.Text = "Reload page";
            this.miRefresh.Click += new System.EventHandler(this.Refresh_Click);
            //
            // miStop
            //
            this.miStop.Name = "miStop";
            this.miStop.Text = "Stop loading (Esc)";
            this.miStop.Click += new System.EventHandler(this.Stop_Click);
            //
            // miSep1
            //
            this.miSep1.Name = "miSep1";
            //
            // miFocusAddress
            //
            this.miFocusAddress.Name = "miFocusAddress";
            this.miFocusAddress.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.miFocusAddress.Text = "Go to address bar";
            this.miFocusAddress.Click += new System.EventHandler(this.FocusAddress_Click);
            //
            // miKeywords
            //
            this.miKeywords.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddKeyword,
            this.miDeleteKeyword});
            this.miKeywords.Name = "miKeywords";
            this.miKeywords.Text = "&Keywords";
            //
            // miAddKeyword
            //
            this.miAddKeyword.Name = "miAddKeyword";
            this.miAddKeyword.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miAddKeyword.Text = "Add keyword...";
            this.miAddKeyword.Click += new System.EventHandler(this.AddKeyword_Click);
            //
            // miDeleteKeyword
            //
            this.miDeleteKeyword.Name = "miDeleteKeyword";
            this.miDeleteKeyword.Text = "Delete selected keyword";
            this.miDeleteKeyword.Click += new System.EventHandler(this.DeleteKeyword_Click);
            //
            // miHelp
            //
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHowTo,
            this.miAbout});
            this.miHelp.Name = "miHelp";
            this.miHelp.Text = "&Help";
            //
            // miHowTo
            //
            this.miHowTo.Name = "miHowTo";
            this.miHowTo.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.miHowTo.Text = "How to use";
            this.miHowTo.Click += new System.EventHandler(this.HowTo_Click);
            //
            // miAbout
            //
            this.miAbout.Name = "miAbout";
            this.miAbout.Text = "About";
            this.miAbout.Click += new System.EventHandler(this.About_Click);
            //
            // navStrip
            //
            this.navStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.navStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.navStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.btnForward,
            this.btnHome,
            this.btnRefresh,
            this.btnStop,
            this.txtAddress,
            this.btnGo});
            this.navStrip.Name = "navStrip";
            this.navStrip.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.navStrip.TabIndex = 1;
            this.navStrip.SizeChanged += new System.EventHandler(this.NavStrip_SizeChanged);
            //
            // btnBack
            //
            this.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBack.Name = "btnBack";
            this.btnBack.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnBack.Text = "◄ Back";
            this.btnBack.ToolTipText = "Go back to the previous page (Alt+Left)";
            this.btnBack.Click += new System.EventHandler(this.Back_Click);
            //
            // btnForward
            //
            this.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnForward.Name = "btnForward";
            this.btnForward.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnForward.Text = "Forward ►";
            this.btnForward.ToolTipText = "Go forward to the next page (Alt+Right)";
            this.btnForward.Click += new System.EventHandler(this.Forward_Click);
            //
            // btnHome
            //
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnHome.Text = "Home";
            this.btnHome.ToolTipText = "Go to the home page (Alt+Home)";
            this.btnHome.Click += new System.EventHandler(this.Home_Click);
            //
            // btnRefresh
            //
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnRefresh.Text = "Reload";
            this.btnRefresh.ToolTipText = "Reload the page (F5)";
            this.btnRefresh.Click += new System.EventHandler(this.Refresh_Click);
            //
            // btnStop
            //
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStop.Name = "btnStop";
            this.btnStop.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnStop.Text = "Stop";
            this.btnStop.ToolTipText = "Stop loading the page (Esc)";
            this.btnStop.Click += new System.EventHandler(this.Stop_Click);
            //
            // txtAddress
            //
            this.txtAddress.AutoSize = false;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(400, 29);
            this.txtAddress.ToolTipText = "Type a website address or search words, then press Enter";
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Address_KeyDown);
            //
            // btnGo
            //
            this.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGo.Name = "btnGo";
            this.btnGo.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnGo.Text = "Go";
            this.btnGo.ToolTipText = "Open the address in the address bar (Enter)";
            this.btnGo.Click += new System.EventHandler(this.Go_Click);
            //
            // keywordStrip
            //
            this.keywordStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.keywordStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.keywordStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.keywordStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblKeywords,
            this.cmbKeywords,
            this.btnAddKeyword,
            this.btnDeleteKeyword});
            this.keywordStrip.Name = "keywordStrip";
            this.keywordStrip.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.keywordStrip.TabIndex = 2;
            //
            // lblKeywords
            //
            this.lblKeywords.Name = "lblKeywords";
            this.lblKeywords.Text = "Blocked keywords (0):";
            //
            // cmbKeywords
            //
            this.cmbKeywords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeywords.DropDownWidth = 260;
            this.cmbKeywords.Name = "cmbKeywords";
            this.cmbKeywords.Size = new System.Drawing.Size(220, 29);
            this.cmbKeywords.ToolTipText = "List of keywords that block websites";
            this.cmbKeywords.SelectedIndexChanged += new System.EventHandler(this.Keywords_SelectedIndexChanged);
            //
            // btnAddKeyword
            //
            this.btnAddKeyword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddKeyword.Name = "btnAddKeyword";
            this.btnAddKeyword.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnAddKeyword.Text = "+ Add keyword";
            this.btnAddKeyword.ToolTipText = "Add a new keyword (Ctrl+N)";
            this.btnAddKeyword.Click += new System.EventHandler(this.AddKeyword_Click);
            //
            // btnDeleteKeyword
            //
            this.btnDeleteKeyword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDeleteKeyword.Name = "btnDeleteKeyword";
            this.btnDeleteKeyword.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnDeleteKeyword.Text = "– Delete keyword";
            this.btnDeleteKeyword.ToolTipText = "Delete the keyword selected in the list";
            this.btnDeleteKeyword.Click += new System.EventHandler(this.DeleteKeyword_Click);
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar,
            this.lblBlocked});
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.TabIndex = 3;
            //
            // lblStatus
            //
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // progressBar
            //
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(140, 16);
            this.progressBar.Visible = false;
            //
            // lblBlocked
            //
            this.lblBlocked.Name = "lblBlocked";
            this.lblBlocked.Text = "Blocked sites: 0";
            //
            // webBrowser1
            //
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.CanGoBackChanged += new System.EventHandler(this.Browser_NavStateChanged);
            this.webBrowser1.CanGoForwardChanged += new System.EventHandler(this.Browser_NavStateChanged);
            this.webBrowser1.DocumentTitleChanged += new System.EventHandler(this.Browser_DocumentTitleChanged);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.Browser_Navigated);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Browser_Navigating);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.Browser_NewWindow);
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.Browser_ProgressChanged);
            this.webBrowser1.StatusTextChanged += new System.EventHandler(this.Browser_StatusTextChanged);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Browser_DocumentCompleted);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 720);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.keywordStrip);
            this.Controls.Add(this.navStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Web Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.navStrip.ResumeLayout(false);
            this.navStrip.PerformLayout();
            this.keywordStrip.ResumeLayout(false);
            this.keywordStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miNavigation;
        private System.Windows.Forms.ToolStripMenuItem miBack;
        private System.Windows.Forms.ToolStripMenuItem miForward;
        private System.Windows.Forms.ToolStripMenuItem miHome;
        private System.Windows.Forms.ToolStripMenuItem miRefresh;
        private System.Windows.Forms.ToolStripMenuItem miStop;
        private System.Windows.Forms.ToolStripSeparator miSep1;
        private System.Windows.Forms.ToolStripMenuItem miFocusAddress;
        private System.Windows.Forms.ToolStripMenuItem miKeywords;
        private System.Windows.Forms.ToolStripMenuItem miAddKeyword;
        private System.Windows.Forms.ToolStripMenuItem miDeleteKeyword;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripMenuItem miHowTo;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStrip navStrip;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripButton btnHome;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripTextBox txtAddress;
        private System.Windows.Forms.ToolStripButton btnGo;
        private System.Windows.Forms.ToolStrip keywordStrip;
        private System.Windows.Forms.ToolStripLabel lblKeywords;
        private System.Windows.Forms.ToolStripComboBox cmbKeywords;
        private System.Windows.Forms.ToolStripButton btnAddKeyword;
        private System.Windows.Forms.ToolStripButton btnDeleteKeyword;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblBlocked;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
