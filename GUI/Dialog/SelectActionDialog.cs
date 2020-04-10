using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class SelectActionDialog : Form
    {
        public SelectActionDialog()
        {
            InitializeComponent();
            Point position = Control.MousePosition;
            position.X -= Size.Width / 2;
            position.Y -= Size.Height / 2;

            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        public ActionSelected Selection { get; private set; }

        private void BlockEdgesButton_Click(object sender, EventArgs e)
        {
            Selection = ActionSelected.BlockEdges;
        }

        private void ScanByRangeTree_Click(object sender, EventArgs e)
        {
            Selection = ActionSelected.RangeScan;
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

        private void SelectActionDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.DialogPosition = Location;
        }
    }
}
