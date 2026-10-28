using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace lab3
{
    /// <summary>
    /// Fereastra in care utilizatorul introduce un cuvant cheie nou.
    /// </summary>
    public class AddKeywordForm : Form
    {
        private readonly TextBox keywordTextBox;
        private readonly List<string> existingKeywords;

        public string Keyword
        {
            get { return keywordTextBox.Text.Trim().ToLowerInvariant(); }
        }

        public AddKeywordForm(IEnumerable<string> existing)
        {
            existingKeywords = existing == null ? new List<string>() : existing.ToList();

            Text = "Add keyword";
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(440, 190);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            Label info = new Label();
            info.Text = "Type a word. Any web address containing this word will be blocked.";
            info.Left = 16;
            info.Top = 14;
            info.Width = 408;
            info.Height = 44;

            Label label = new Label();
            label.Text = "Keyword:";
            label.Left = 16;
            label.Top = 68;
            label.Width = 110;

            keywordTextBox = new TextBox();
            keywordTextBox.Left = 130;
            keywordTextBox.Top = 64;
            keywordTextBox.Width = 294;
            keywordTextBox.MaxLength = 50;

            Label example = new Label();
            example.Text = "Example: facebook";
            example.ForeColor = SystemColors.GrayText;
            example.Left = 130;
            example.Top = 96;
            example.Width = 294;

            Button okButton = new Button();
            okButton.Text = "Add";
            okButton.Left = 232;
            okButton.Top = 140;
            okButton.Width = 94;
            okButton.Height = 32;
            okButton.DialogResult = DialogResult.OK;
            okButton.Click += ValidateKeyword;

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Left = 332;
            cancelButton.Top = 140;
            cancelButton.Width = 92;
            cancelButton.Height = 32;
            cancelButton.DialogResult = DialogResult.Cancel;

            Controls.Add(info);
            Controls.Add(label);
            Controls.Add(keywordTextBox);
            Controls.Add(example);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        private void ValidateKeyword(object sender, EventArgs e)
        {
            string text = keywordTextBox.Text.Trim();
            string problem = null;

            if (text.Length == 0)
                problem = "Please type a word first.";
            else if (text.Any(char.IsWhiteSpace))
                problem = "Enter a single word, without spaces.";
            else if (text.Length < 3)
                problem = "The word must have at least 3 letters, otherwise it would block too many websites.";
            else if (existingKeywords.Any(x => string.Equals(x, text, StringComparison.OrdinalIgnoreCase)))
                problem = "The word \"" + text + "\" is already in the list.";

            if (problem != null)
            {
                MessageBox.Show(this, problem, "Invalid keyword",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                keywordTextBox.Focus();
                keywordTextBox.SelectAll();
            }
        }
    }
}
