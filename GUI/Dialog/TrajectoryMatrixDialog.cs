using ForestGraph;
using GraphService.Dijkstra;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class TrajectoryMatrixDialog : Form
    {
        ForestGraph<string, VertexData, string, EdgeData> _graph;
        DijkstraAlhorithm<string, VertexData, string, EdgeData> _dijkstra;

        public TrajectoryMatrixDialog(ForestGraph<string, VertexData, string, EdgeData> graph, DijkstraAlhorithm<string, VertexData, string, EdgeData> dijkstra)
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

            List<string> stops = new List<string>();
            List<string> restAreas = new List<string>();

            //Get vertexes separated by type
            foreach(var vertex in _graph.GetAllVertexes())
            {
                if(vertex.data.VertexType == VertexType.Stop)
                {
                    stops.Add(vertex.key);
                }
                if(vertex.data.VertexType == VertexType.RestArea)
                {
                    restAreas.Add(vertex.key);
                }
            }

            //Generated matrix
            if (stops.Count >= 1 && restAreas.Count >=1)
            {
                warnLabel.Visible = false;
                MatrixGrid.ColumnCount = stops.Count;

                MatrixGrid.Rows.Add(restAreas.Count);
                
                //rows
                for (int i = 0; i< restAreas.Count; i++)
                {
                    MatrixGrid.Rows[i].HeaderCell.Value = restAreas[i];
                    _dijkstra.FindPaths(restAreas[i], true); // Generate all paths from start

                    //columns
                    for (int l = 0; l < stops.Count; l++)
                    {
                        MatrixGrid.Columns[l].Name = stops[l];

                        var path = _dijkstra.GetPath(stops[l]);

                        if(path != null)
                        {
                            float cost = 0; 

                            // Calculate path cost
                            for (int v = 1; v < path.Count; v++)
                            {
                                cost += _graph.FindEdge(path[v - 1], path[v]).Distance;
                            }
                            MatrixGrid.Rows[i].Cells[l].Value = path.Count.ToString() + " (" + cost + ")";
                        }
                        else
                        {
                            MatrixGrid.Rows[i].Cells[l].Value = "-";
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
    }
}
