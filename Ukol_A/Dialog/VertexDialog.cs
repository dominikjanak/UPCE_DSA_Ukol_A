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
    }
}
