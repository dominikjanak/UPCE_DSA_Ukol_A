using System.Windows.Forms;

namespace GUI
{
    public class DoubleBufferedDataGridView : DataGridView
    {
        public DoubleBufferedDataGridView()
        {
            this.DoubleBuffered = true;
        }
    }
}
