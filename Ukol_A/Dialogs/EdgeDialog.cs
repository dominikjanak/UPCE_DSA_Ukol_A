using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ukol_A.Dialog
{
    public partial class EdgeDialog : Form
    {
        public string Key { get; set; }
        public string StartVertex { get; set; }
        public string TargetVertex { get; set; }
        public float Distance { get; set; }

        public EdgeDialog()
        {
            InitializeComponent();
            this.ActiveControl = StartTextbox;
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            float number;

            Key = KeyTextbox.Text;
            StartVertex = StartTextbox.Text;
            TargetVertex = TargetTextbox.Text;

            if (float.TryParse(DistanceTextbox.Text, out number))
            {
                Distance = number;
            }
        }

        private void EdgeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool keyValid = ValidateText(KeyTextbox);
            bool startValid = ValidateText(StartTextbox);
            bool targetValid = ValidateText(TargetTextbox);
            bool distanceValid = ValidateFloat(DistanceTextbox);
            if (DialogResult.OK == this.DialogResult && (!keyValid || !startValid || !targetValid || !distanceValid))
            {
                e.Cancel = true;
            }
        }

        private void KeyTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText(KeyTextbox);
        }

        private void StartTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText(StartTextbox);
            KeyTextbox.Text = StartTextbox.Text + "_" + TargetTextbox.Text;
        }

        private void TargetTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText(TargetTextbox);
            KeyTextbox.Text = StartTextbox.Text + "_" + TargetTextbox.Text;
        }

        private void DistanceTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateFloat(DistanceTextbox);
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

                if (!ValidateFloat(DistanceTextbox, false))
                {
                    DistanceTextbox.Focus();
                    submit = false;
                }

                if (!ValidateText(KeyTextbox, false))
                {
                    KeyTextbox.Focus();
                    submit = false;
                }

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
