using GraphService;
using GraphService.Dijkstra;
using GUI.Drawing;
using GUI.Graph;
using GUI.Graph.Component;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class TrajectoryMatrixDialog : Form
    {
        TrajectoryMatrix _matrix;
        Graph<string, VertexData, string, EdgeData> _graph;
        DijkstraAlhorithm<string, VertexData, string, EdgeData> _dijkstra;

        public TrajectoryMatrixDialog(TrajectoryMatrix matrix)
        {
            InitializeComponent();
            _matrix = matrix;
            DrawMatrix();

            Size size = Properties.Settings.Default.MatrixformSize;
            if (size.Width >= 330 && size.Height >= 200)
            {
                Size = size;
            }

            Point position = Properties.Settings.Default.MatrixformPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        private void DrawMatrix()
        {
            MatrixGrid.ColumnCount = _matrix.ColumnsCount();
            MatrixGrid.Rows.Add(_matrix.RowsCount());

            if(_matrix.ColumnsCount() > 0 && _matrix.RowsCount() >= 0)
            {
                warnLabel.Visible = false;
                // for each column
                for (int col = 0; col < _matrix.ColumnsCount(); col++)
                {
                    MatrixGrid.Columns[col].Name = _matrix.GetColumnKey(col); // print Header
                    MatrixGrid.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;

                    // for each row
                    for (int row = 0; row < _matrix.RowsCount(); row++)
                    {
                        // print Header
                        if (col == 0)
                        {
                            MatrixGrid.Rows[row].HeaderCell.Value = _matrix.GetRowKey(row);
                            //MatrixGrid.Rows[row].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                            MatrixGrid.Rows[row].HeaderCell.Style.SelectionBackColor = Colors.Red;
                        }
                        var val = _matrix[col, row]; // get target ID
                        MatrixGrid.Rows[row].Cells[col].Value = val; // set cell value

                        // if value in cell is same with column name
                        if (val == MatrixGrid.Columns[col].Name)
                        {
                            MatrixGrid.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(220, 255, 220);
                        }
                    }
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

        private void TrajectoryMatrixButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void TrajectoryMatrixDialog_ResizeEnd(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.MatrixformSize = this.Size;
        }

        private void TrajectoryMatrixDialog_Move(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.MatrixformPosition = this.Location;
        }

        private void MatrixGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            MatrixGrid.ClearSelection();
        }

        private void MatrixGrid_Paint(object sender, PaintEventArgs e)
        {
            if(MatrixGrid.CurrentCell != null)
            {
                MatrixGrid.CurrentCell.Selected = false;
            }
        }
    }
}
