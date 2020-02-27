using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ForestGraph;
using GraphService.Dijkstra;
using GUI.Dialog;
using GUI.Drawing;
using GUI.Properties;

namespace GUI
{
    public partial class MainForm : Form
    {
        private DrawGraph _drawer;
        private RectangleF _rectangle;
        private bool _rectangleDrawing;
        private ForestGraph<string, VertexData, string, EdgeData> _forestGraph;
        private String _autoloadPath;
        private bool _saved;
        private List<string> _graphPath;
        private DijkstraAlhorithm<string, VertexData, string, EdgeData> _dijkstra;
        TrajectoryMatrixDialog _matrixDialog;

        public MainForm()
        {
            InitializeComponent();
            _matrixDialog = null;
            Text = @"UPCE - " + AssemblyData.Product;
            _drawer = new DrawGraph();
            _rectangle = new Rectangle(0, 0, 0, 0);
            _rectangleDrawing = false;
            _forestGraph = new ForestGraph<string, VertexData, string, EdgeData>();
            _saved = true;
            _graphPath = new List<string>();
            _dijkstra = new DijkstraAlhorithm<string, VertexData, string, EdgeData>(_forestGraph,
                weight => weight.Distance, through => through.EdgeType == EdgeType.Blocked);

#if DEBUG
            _autoloadPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
#else
            _autoloadPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
#endif
            _autoloadPath += @"\autoload.xml";

            // autoload data
            if (File.Exists(_autoloadPath))
            {
                DataManipulator.LoadData(_forestGraph, _autoloadPath);
                graphCanvas.Invalidate();
            }

            Size size = Properties.Settings.Default.MainformSize;
            if (size.Width >= 500 && size.Height >= 300)
            {
                Size = size;
            }

            Point position = Properties.Settings.Default.MainformPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        private void NewGraphButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == AskForSave())
            {
                return;
            }

            _dijkstra.Invalidate();
            _rectangle = new RectangleF(0, 0, 0, 0);
            _forestGraph.Clear();
            _graphPath.Clear();
            _saved = true;
            graphCanvas.Invalidate();
        }

        private DialogResult AskForSave()
        {
            if (_saved)
            {
                return DialogResult.None;
            }

            DialogResult result = MessageBox.Show(Resources.UNSAVED_CHANGES_ASK_TO_SAVE,
                Resources.UNSAVED_CHANGES, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (DialogResult.Yes == result)
            {
                DataManipulator.SaveDataDialog(_forestGraph);
            }

            return result;
        }

        private void AutoloadSaveButton_Click(object sender, EventArgs e)
        {
            if (DataManipulator.SaveData(_forestGraph, _autoloadPath))
            {
                _saved = false;
            }
        }

        private void AutoloadLoadButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(_autoloadPath))
            {
                _graphPath.Clear();
                DataManipulator.LoadData(_forestGraph, _autoloadPath);
                graphCanvas.Invalidate();
                _dijkstra.Invalidate();
            }
        }

        private void FindRouteButton_Click(object sender, EventArgs e)
        {
            SelectTwoVertexesDialog selectPath = new SelectTwoVertexesDialog(true);
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectPath.ShowDialog())
                {
                    // start and target points are same
                    if (selectPath.StartVertex == selectPath.TargetVertex &&
                        _forestGraph.HasVertex(selectPath.TargetVertex))
                    {
                        ShowMessage(Resources.PATH_TO_SAME_VETEX, MessageBoxIcon.Information);
                        continue;
                    }

                    // start or target point (or both) do not exist
                    if (!_forestGraph.HasVertex(selectPath.StartVertex) ||
                        !_forestGraph.HasVertex(selectPath.TargetVertex))
                    {
                        ShowMessage(Resources.SELECTED_VERTEXES_NOT_EXITS, MessageBoxIcon.Warning);
                        continue;
                    }

                    try
                    {
                        // find and store path
                        _graphPath.Clear();
                        _graphPath = _dijkstra.FindPaths(selectPath.StartVertex).GetPath(selectPath.TargetVertex);
                        graphCanvas.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message);
                        continue;
                    }

                    graphCanvas.Invalidate();
                }

                repeate = false;
            } while (repeate);
        }

        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            VertexDialog newVertex = new VertexDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == newVertex.ShowDialog())
                {
                    VertexData data = new VertexData(new PointF(newVertex.X, newVertex.Y), newVertex.Type);
                    _graphPath.Clear();

                    try
                    {
                        // add vertex
                        _forestGraph.AddVertex(newVertex.Key, data);
                        _saved = false;
                        _dijkstra.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message);
                        continue;
                    }

                    graphCanvas.Invalidate();
                }

                repeate = false;
            } while (repeate);
        }

        private void RemoveVertexButton_Click(object sender, EventArgs e)
        {
            SelectVertexDialog selectVertex = new SelectVertexDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectVertex.ShowDialog())
                {
                    try
                    {
                        // remove vertex
                        _forestGraph.RemoveVertex(selectVertex.Key);
                        _graphPath.Clear();
                        _saved = false;
                        _dijkstra.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message);
                        continue;
                    }

                    graphCanvas.Invalidate();
                }

                repeate = false;
            } while (repeate);
        }

        private void AddEdgeButton_Click(object sender, EventArgs e)
        {
            EdgeDialog newEdge = new EdgeDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == newEdge.ShowDialog())
                {
                    EdgeData data = new EdgeData(newEdge.Distance, EdgeType.Free);

                    try
                    {
                        // add edge
                        _forestGraph.AddEdge(newEdge.Key, newEdge.StartVertex, newEdge.TargetVertex, data);
                        _graphPath.Clear();
                        _saved = false;
                        _dijkstra.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message);
                        continue;
                    }

                    graphCanvas.Invalidate();
                }

                repeate = false;
            } while (repeate);
        }

        private void RemoveEdgeButton_Click(object sender, EventArgs e)
        {
            SelectTwoVertexesDialog selectEdge = new SelectTwoVertexesDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectEdge.ShowDialog())
                {
                    try
                    {
                        // remove edge
                        _forestGraph.RemoveEdge(selectEdge.StartVertex, selectEdge.TargetVertex);
                        _graphPath.Clear();
                        _saved = false;
                        _dijkstra.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message);
                        continue;
                    }

                    graphCanvas.Invalidate();
                }

                repeate = false;
            } while (repeate);
        }

        private void AboutProgramButton_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
        
        public void MatrixOnThread()
        {
            _matrixDialog?.Close();
            _matrixDialog = new TrajectoryMatrixDialog(_forestGraph, _dijkstra);
            _matrixDialog.ShowDialog();
        }

        private void TrajectoryMatrixButton_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(MatrixOnThread));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void ShowMessage(string message, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(message, Resources.ERROR_LABEL, MessageBoxButtons.OK, icon);
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            SaveImageManager(graphCanvas);
        }

        public void SaveImageManager(Panel canvas)
        {
            SaveFileDialog saveImage = new SaveFileDialog()
            {
                FileName = "Graph_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"),
                Filter = @"PNG Image|*.png|JPEG Image|*.jpeg|GIF Image|*.gif|Bitmap Image|*.bmp",
                DefaultExt = "png",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            // save graph as an image
            if (saveImage.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveImage.FileName;

                int width = canvas.Size.Width;
                int height = canvas.Size.Height;

                Bitmap bitmap = new Bitmap(width, height);
                var drawer = new DrawGraph(bitmap);
                _forestGraph.DrawWithPath(drawer, _graphPath);

                bitmap.Save(filePath);
                Process.Start(filePath);
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveDataButton_Click(object sender, EventArgs e)
        {
            if (DataManipulator.SaveDataDialog(_forestGraph))
            {
                _saved = true;
            }
        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            _graphPath.Clear();
            DataManipulator.LoadData(_forestGraph);
            graphCanvas.Invalidate();
            _dijkstra.Invalidate();
        }

        private void graphCanvas_Paint(object sender, PaintEventArgs e)
        {
            // draw graph
            _drawer.InitCanvas(graphCanvas.Size, e.Graphics);
            _forestGraph.DrawWithPath(_drawer, _graphPath);

            // draw rectangle
            if (!_rectangle.Location.IsEmpty && !_rectangle.Size.IsEmpty && _rectangleDrawing)
            {
                RectangleF rectangle = new RectangleF(_rectangle.Location, _rectangle.Size);
                _drawer.DrawRectangle(rectangle);
            }
        }

        private void graphCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Get start of rectangle
            if (e.Button == MouseButtons.Left)
            {
                _rectangleDrawing = true;

                PointF location = e.Location;
                _drawer.Denormalize(ref location);
                _rectangle.Location = location;
            }

            _rectangle.Size = SizeF.Empty;
        }

        private void graphCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            // Get end of rectangle
            if (e.Button == MouseButtons.Left)
            {
                _rectangleDrawing = false;
                UpdateEdgeType();
                _graphPath.Clear();
                graphCanvas.Invalidate();
                _dijkstra.Invalidate();
            }
        }

        private void graphCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Draw rectangle while mouse button is pressed
            if (_rectangleDrawing)
            {
                PointF location = e.Location;
                _drawer.Denormalize(ref location);

                _rectangle.Width = location.X - _rectangle.Location.X;
                _rectangle.Height = location.Y - _rectangle.Location.Y;

                graphCanvas.Invalidate();
            }
        }

        private void UpdateEdgeType()
        {
            var edges = _forestGraph.GetAllEdges();

            // all edges are free
            foreach (var edge in edges)
            {
                edge.data.EdgeType = EdgeType.Free;
            }

            var vertexes = _forestGraph.GetAllVertexes();
            
            //Analyse all vertexes
            foreach (var vertex in vertexes)
            {
                var loc = vertex.data.Location;
                RectangleF rect = _rectangle;

                // recalculate rectangle if width < 0
                if (rect.Width < 0)
                {
                    rect.Width = -rect.Width;
                    rect.X -= rect.Width;
                }
                // recalculate rectangle if height < 0
                if (_rectangle.Height < 0)
                {
                    rect.Height = -rect.Height;
                    rect.Y -= rect.Height;
                }

                // if vertex is in restangle
                if (loc.X >= rect.Left && loc.X <= rect.Left + rect.Width &&
                    loc.Y >= rect.Top && loc.Y <= rect.Top + rect.Height)
                {
                    var incidentEdges = _forestGraph.GetVertexIncidents(vertex.key);

                    // All edges from vertex will be blocked 
                    foreach (var edge in incidentEdges)
                    {
                        edge.data.EdgeType = EdgeType.Blocked;
                    }
                }
            }
        }

        private void graphCanvas_Resize(object sender, EventArgs e)
        {
            graphCanvas.Invalidate();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.MainformPosition = this.Location;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.MainformSize = this.Size;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Cancel == AskForSave())
            {
                e.Cancel = true;
            }
        }

        // Save data after press Ctrl + S
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                if (keyData == (Keys.Control | Keys.S))
                {
                    if (DataManipulator.SaveData(_forestGraph, _autoloadPath))
                    {
                        _saved = false;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}