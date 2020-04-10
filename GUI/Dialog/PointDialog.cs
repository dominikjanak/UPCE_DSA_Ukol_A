using GUI.Graph;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class PointDialog : Form
    {
        public float X { get; set; }
        public float Y { get; set; }


        public PointDialog()
        {
            InitializeComponent();
            Point position = Properties.Settings.Default.DialogPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            float number;
            if (float.TryParse(XTextbox.Text, out number))
            {
                X = number;
            }
            if (float.TryParse(YTextbox.Text, out number))
            {
                Y = number;
            }
        }

        private void PointDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool xValid = ValidateFloat(XTextbox);
            bool yValid = ValidateFloat(YTextbox);
            if (DialogResult.OK == this.DialogResult && (!xValid || !yValid))
            {
                e.Cancel = true;
            }
        }

        private void XTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateFloat(XTextbox);
        }

        private void YTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateFloat(YTextbox);
        }

        private bool ValidateFloat(TextBox box, bool highlight = true)
        {
            float f;
            if (float.TryParse(box.Text, out f))
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

                if (!ValidateFloat(YTextbox, false))
                {
                    YTextbox.Focus();
                    submit = false;
                }

                if (!ValidateFloat(XTextbox, false))
                {
                    XTextbox.Focus();
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

        private void PointDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.DialogPosition = Location;
        }
    }
}
