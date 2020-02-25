using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class VertexDialog : Form
    {
        public string Key { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public VertexType Type { get; set; }


        public VertexDialog()
        {
            InitializeComponent();
            ComboVertexType.SelectedIndex = 0;
            this.ActiveControl = ComboVertexType;
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            float number;

            Key = KeyTextbox.Text;

            if (float.TryParse(XTextbox.Text, out number))
            {
                X = number;
            }
            if (float.TryParse(YTextbox.Text, out number))
            {
                Y = number;
            }

            switch (ComboVertexType.SelectedIndex)
            {
                case 0:
                    Type = VertexType.Junction;
                    break;
                case 1:
                    Type = VertexType.RestArea;
                    break;
                case 2:
                    Type = VertexType.Stop;
                    break;
            }
        }

        private void VertexDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool xValid = ValidateFloat(XTextbox);
            bool yValid = ValidateFloat(YTextbox);
            bool keyValid = ValidateText(KeyTextbox);
            if (DialogResult.OK == this.DialogResult && (!xValid || !yValid || !keyValid))
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

        private void KeyTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText(KeyTextbox);
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
    }
}
