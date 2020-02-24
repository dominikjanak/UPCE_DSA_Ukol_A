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
    public partial class SelectVertexDialog : Form
    {
        public string Key { get; set; }


        public SelectVertexDialog()
        {
            InitializeComponent();
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            Key = KeyTextbox.Text;
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

        private void SelectVertexDialog_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
