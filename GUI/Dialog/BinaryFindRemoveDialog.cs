using BinaryDataStorageEngine;
using GUI.Graph;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class BinaryFindRemoveDialog : Form
    {
        public string Key { get; private set; }
        public SearchMethod Method { get; private set; }

        public BinaryFindRemoveDialog(bool findDialog = true)
        {
            InitializeComponent();
            ComboType.SelectedIndex = 0;
            this.ActiveControl = ComboType;

            if (!findDialog)
            {
                FormLabel.Text = "Odstranit vrchol";
                DialogSubmitButton.Text = "Odstranit";
            }

            Point position = Properties.Settings.Default.DialogPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            Key = KeyTextbox.Text;

            switch (ComboType.SelectedIndex)
            {
                case 0:
                    Method = SearchMethod.Interpolation;
                    break;
                case 1:
                    Method = SearchMethod.Binary;
                    break;
            }
        }

        private void BinaryFindRemoveDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool keyValid = ValidateText(KeyTextbox);
            if (DialogResult.OK == this.DialogResult && (!keyValid))
            {
                e.Cancel = true;
            }
        }

        private void KeyTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText(KeyTextbox);
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
                e.Handled = true;
                e.SuppressKeyPress = true;
                bool submit = true;

                if (!ValidateText(KeyTextbox, false))
                {
                    KeyTextbox.Focus();
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

        private void BinaryFindRemoveDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.DialogPosition = Location;
        }
    }
}
