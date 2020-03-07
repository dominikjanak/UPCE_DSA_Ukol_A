using GraphService;
using GraphService.Dijkstra;
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

        public TrajectoryMatrixDialog(Graph<string, VertexData, string, EdgeData> graph, DijkstraAlhorithm<string, VertexData, string, EdgeData> dijkstra)
        {
            InitializeComponent();
            _graph = graph;
            _dijkstra = dijkstra;
            CalculateMatrix();

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

        private void CalculateMatrix()
        {
            var allVerticies = _graph.Vertices;
            _matrix = new TrajectoryMatrix(allVerticies);

            List<string> stops = _matrix.GetAllStops();
            List<string> restAreas = _matrix.GetAllRestAreas();
            
            if (stops.Count >= 1 && restAreas.Count >= 1)
            {
                warnLabel.Visible = false;

                for (int restIdx = 0; restIdx < restAreas.Count; restIdx++)
                {
                    _dijkstra.FindPaths(restAreas[restIdx], true); // Generate all paths from start

                    for (int stopIdx = 0; stopIdx < stops.Count; stopIdx++)
                    {
                        var targetStop = stops[stopIdx];
                        List<string> path = _dijkstra.GetPath(targetStop);

                        if (path != null)
                        {
                            // add path to matrix
                            for (int v = 1; v < path.Count; v++)
                            {
                                _matrix[targetStop, path[v - 1]] = path[v];
                            }
                        }

                    }
                }
                DrawMatrix();
            }
        }

        private void DrawMatrix()
        {
            MatrixGrid.ColumnCount = _matrix.ColumnsCount();
            MatrixGrid.Rows.Add(_matrix.RowsCount());

            // for each column
            for (int col = 0; col < _matrix.ColumnsCount(); col++)
            {
                MatrixGrid.Columns[col].Name = _matrix.GetColumnKey(col); // print Header

                // for each row
                for (int row = 0; row < _matrix.RowsCount(); row++)
                {
                    // print Header
                    if (col == 0)
                    {
                        MatrixGrid.Rows[row].HeaderCell.Value = _matrix.GetRowKey(row);
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
    }
}
