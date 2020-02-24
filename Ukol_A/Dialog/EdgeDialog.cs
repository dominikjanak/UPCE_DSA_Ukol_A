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

        private bool ValidateFloat(TextBox box)
        {
            float f;
            if (float.TryParse(box.Text, out f))
            {
                box.BackColor = Color.White;
                return true;
            }
            else
            {
                box.BackColor = Color.FromArgb(255,200,200);
            }
            return false;
        }

        private bool ValidateText(TextBox box)
        {
            if (box.Text.Length > 0)
            {
                box.BackColor = Color.White;
                return true;
            }
            else
            {
                box.BackColor = Color.FromArgb(255,200,200);
            }
            return false;
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
        }

        private void TargetTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText(TargetTextbox);
        }

        private void DistanceTextbox_TextChanged(object sender, EventArgs e)
        {
            ValidateFloat(DistanceTextbox);
        }
    }
}
