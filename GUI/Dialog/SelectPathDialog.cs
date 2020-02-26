using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class SelectPathDialog : Form
    {
        public string Start { get; set; }
        public string Target { get; set; }


        public SelectPathDialog()
        {
            InitializeComponent();
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            Start = StartTextbox.Text;
            Target = TargetTextbox.Text;
        }

        private void SelectPathDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool startValid = ValidateText(StartTextbox);
            bool targetValid = ValidateText(TargetTextbox);
            if (DialogResult.OK == this.DialogResult && (!startValid || !targetValid))
            {
                e.Cancel = true;
            }
        }

        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText((TextBox)sender);
        }

        private bool ValidateText(TextBox box, bool highlight = true)
        {
            if (box.Text.Length > 0)
            {
                if (highlight)
                {
                    box.BackColor = Color.White;
                }
                return true;
            }
            else
            {
                if (highlight)
                {
                    box.BackColor = Color.FromArgb(255, 200, 200);
                }
            }
            return false;
        }

        private void FormSubmit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool submit = true;

                if (!ValidateText(TargetTextbox, false))
                {
                    TargetTextbox.Focus();
                    submit = false;
                }

                if (!ValidateText(StartTextbox, false))
                {
                    StartTextbox.Focus();
                    submit = false;
                }

                if (submit)
                {
                    this.DialogSubmitButton.PerformClick();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                if (keyData == Keys.Escape)
                {
                    this.Close();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
