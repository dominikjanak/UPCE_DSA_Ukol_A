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
    public partial class SelectEdgeDialog : Form
    {
        public string StartVertex { get; set; }
        public string TargetVertex { get; set; }


        public SelectEdgeDialog()
        {
            InitializeComponent();
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            StartVertex = StartTextbox.Text;
            TargetVertex = TargetTextbox.Text;
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

        private void SelectEdgeDialog_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
