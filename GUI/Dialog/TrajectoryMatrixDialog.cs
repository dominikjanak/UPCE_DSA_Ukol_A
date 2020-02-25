using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class TrajectoryMatrixDialog : Form
    {
        public TrajectoryMatrixDialog()
        {
            InitializeComponent();
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
