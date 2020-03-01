using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();
            Point position = Properties.Settings.Default.HelpPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
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

        private void VertexDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.HelpPosition = Location;
        }
    }
}
