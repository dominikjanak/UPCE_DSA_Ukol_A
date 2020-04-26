using GUI.Drawing;
using GUI.Graph;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class BinaryGenerateDialog : Form
    {
        public int Count { get; set; }

        public BinaryGenerateDialog()
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
            int number;

            if (int.TryParse(NumberTextbox.Text, out number))
            {
                Count = number;
            }
        }

        private void BinaryGenerateDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool numberValid = ValidatePositiveInt(NumberTextbox);
            if (DialogResult.OK == this.DialogResult && (!numberValid))
            {
                e.Cancel = true;
            }
        }

        private void NumberTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidatePositiveInt(NumberTextbox);
        }

        private bool ValidatePositiveInt(TextBox box, bool highlight = true)
        {
            int num;
            Color color = Colors.White;
            bool state = true;


            if (int.TryParse(box.Text, out num))
            {
                if (num < 0)
                {
                    color = Color.FromArgb(255, 200, 200);
                    state = false;
                }
            }
            else
            {
                color = Color.FromArgb(255, 200, 200);
                state = false;
            }

            if (highlight)
            {
                box.BackColor = color;
            }
            return state;
        }

        private void FormSubmit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                bool submit = true;

                if (!ValidatePositiveInt(NumberTextbox, false))
                {
                    NumberTextbox.Focus();
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

        private void BinaryGenerateDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.DialogPosition = Location;
        }
    }
}
