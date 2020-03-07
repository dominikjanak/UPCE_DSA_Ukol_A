using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GraphService;
using GraphService.Dijkstra;
using GUI.Dialog;
using GUI.Graph;
using GUI.Graph.Component;
using GUI.Graph.Drawing;
using GUI.Properties;

namespace GUI
{
    public partial class MainForm : Form
    {
        private DrawGraph _drawer;
        private RectangleF _rectangle;
        private bool _rectangleDrawing;
        private Graph<string, VertexData, string, EdgeData> _forestGraph;
        private String _autoloadPath;
        private bool _saved;
        private List<string> _graphPath;
        private DijkstraAlhorithm<string, VertexData, string, EdgeData> _dijkstra;
        TrajectoryMatrixDialog _matrixDialog;
        private string _saveFileName;

        public MainForm()
        {
            InitializeComponent();
            _matrixDialog = null;
            _drawer = new DrawGraph();
            _rectangle = new Rectangle(0, 0, 0, 0);
            _rectangleDrawing = false;
            _forestGraph = new Graph<string, VertexData, string, EdgeData>();
            _saved = true;
            _graphPath = new List<string>();
            _dijkstra = new DijkstraAlhorithm<string, VertexData, string, EdgeData>(_forestGraph);
            _saveFileName = "";
            SetTitle();

#if DEBUG
            _autoloadPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
#else
            _autoloadPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
#endif
            _autoloadPath += @"\autoload.xml";

            // autoload data
            if (File.Exists(_autoloadPath))
            {
                try
                {
                    DataSerializer.LoadData(_forestGraph, _autoloadPath);
                    graphCanvas.Invalidate();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(Resources.AutoImportError + "\n\nDetail chyby:\n" + ex.Message, Resources.AutoImportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void SetTitle(string file = "")
        {
            Text = @"UPCE - " + AssemblyData.Product;
            if (!string.IsNullOrEmpty(file))
            {
                Text += " ["+file+"]";
            }
        }

        private DialogResult AskForSave()
        {
            if (_saved)
            {
                return DialogResult.None;
            }

            DialogResult result = MessageBox.Show(Resources.UnsavedChangesQuestionToSave,
                Resources.UnsavedChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (DialogResult.Yes == result)
            {
                try
                {
                    _saveFileName = DataSerializer.SaveDataDialog(_forestGraph, _saveFileName);
                    SetTitle(Path.GetFileName(_saveFileName));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(Resources.ExportError + "\n" + ex.Message, Resources.ExportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return result;
        }

        private void ShowMessage(string message, MessageBoxIcon icon = MessageBoxIcon.Error, string title = "")
        {
            MessageBox.Show(message, String.IsNullOrEmpty(title)?Resources.ErrorLabel : title, MessageBoxButtons.OK, icon);
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
            _saveFileName = "";
            SetTitle();
        }

        private void Autoload_SaveButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(_autoloadPath))
            {
                DialogResult result = MessageBox.Show(Resources.AutoloadFileExists, Resources.AutoloadFileExistsTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {

                if (DataSerializer.SaveData(_forestGraph, _autoloadPath))
                {
                    _saved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ExportError + ex.Message, Resources.ExportErrorTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Autoload_LoadButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(_autoloadPath))
            {
                try
                {
                    _graphPath.Clear();
                    DataSerializer.LoadData(_forestGraph, _autoloadPath);
                    graphCanvas.Invalidate();
                    _dijkstra.Invalidate();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(Resources.ImportError + ex.Message, Resources.ImportErrorTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Resources.ImportFileNotExists, Resources.ImportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindRouteButton_Click(object sender, EventArgs e)
        {
            SelectTwoVerticesDialog selectPath = new SelectTwoVerticesDialog(true);
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectPath.ShowDialog())
                {
                    // start and target points are same
                    if (selectPath.StartVertex == selectPath.TargetVertex &&
                        _forestGraph.HasVertex(selectPath.TargetVertex))
                    {
                        ShowMessage(Resources.PathToSameVertex, MessageBoxIcon.Information);
                        continue;
                    }

                    // start or target point (or both) do not exist
                    if (!_forestGraph.HasVertex(selectPath.StartVertex) ||
                        !_forestGraph.HasVertex(selectPath.TargetVertex))
                    {
                        ShowMessage(Resources.SelectedVerticesNotExist, MessageBoxIcon.Warning);
                        continue;
                    }

                    try
                    {
                            // find and store path
                            _graphPath.Clear();
                            var path = _dijkstra.FindPaths(selectPath.StartVertex).GetPath(selectPath.TargetVertex);
                            if (path.Count > 0)
                            {
                                _graphPath = path;
                                graphCanvas.Invalidate();
                            }
                            else
                            {
                                ShowMessage(Resources.PathNotExists, MessageBoxIcon.Warning);
                            }
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
            SelectTwoVerticesDialog selectEdge = new SelectTwoVerticesDialog();
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

        private void ProgramHelpButton_Click(object sender, EventArgs e)
        {
            HelpDialog help = new HelpDialog();
            help.ShowDialog();
        }

        private void TrajectoryMatrixButton_Click(object sender, EventArgs e)
        {
            if(_matrixDialog != null)
            {
                _matrixDialog.Close();
            }

            _matrixDialog = new TrajectoryMatrixDialog(_forestGraph, _dijkstra);
            _matrixDialog.Show();
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string path = DataSerializer.SaveDataDialog(_forestGraph, _saveFileName);
            if (!string.IsNullOrEmpty(path))
            {
                _saveFileName = path;
                _saved = true;
                SetTitle(Path.GetFileName(_saveFileName));
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = DataSerializer.SaveDataDialog(_forestGraph);
                if (!string.IsNullOrEmpty(path))
                {
                    _saveFileName = path; 
                    _saved = true;
                    SetTitle(Path.GetFileName(_saveFileName));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Resources.ExportError + "\n" + ex.Message, Resources.ExportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = DataSerializer.LoadDataDialog(_forestGraph);
                if (!string.IsNullOrEmpty(path))
                {
                    _graphPath.Clear();
                    graphCanvas.Invalidate();
                    _dijkstra.Invalidate();

                    _saveFileName = path;
                    _saved = true;
                    SetTitle(Path.GetFileName(_saveFileName));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Resources.ImportError + "\n" + ex.Message, Resources.ImportErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateGraphStrip_Click(object sender, EventArgs e)
        {
            GenerateDialog genDialog = new GenerateDialog();

            if(genDialog.ShowDialog() == DialogResult.OK) 
            { 
                if (_forestGraph.VerticesCount() > 0 && !_saved)
                {
                    DialogResult result = MessageBox.Show(Resources.DataAlreadyExists, 
                        Resources.DataAlreadyExistsTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                GraphGenerator.Init(_forestGraph);
                GraphGenerator.Generate((genDialog.JunctionsCount, genDialog.RestAreasCount, 
                    genDialog.StopsCount), genDialog.MinVertexEdge, genDialog.MaxVertexEdge);

                _dijkstra.Invalidate();
                _rectangle = new RectangleF(0, 0, 0, 0);
                _graphPath.Clear();
                _saved = true;
                graphCanvas.Invalidate();
            }
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
            var edges = _forestGraph.Edges;

            // all edges are free
            foreach (var edge in edges)
            {
                edge.Data.EdgeType = EdgeType.Free;
            }

            var vertices = _forestGraph.Vertices;
            
            //Analyse all vertices
            foreach (var vertex in vertices)
            {
                var loc = vertex.Data.Location;
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
                    var incidentEdges = _forestGraph.VertexIncidents(vertex.Key);

                    // All edges from vertex will be blocked 
                    foreach (var edge in incidentEdges)
                    {
                        edge.Data.EdgeType = EdgeType.Blocked;
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

        // Program shortcuts
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                if (keyData == (Keys.Control | Keys.N))
                {
                    NewGraphButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.S))
                {
                    SaveButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.D))
                {
                    SaveAsButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.O))
                {
                    LoadButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.F))
                {
                    FindRouteButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.G))
                {
                    GenerateGraphStrip_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.R))
                {
                    AddVertexButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.E))
                {
                    AddEdgeButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.Shift | Keys.R))
                {
                    RemoveVertexButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.Shift | Keys.E))
                {
                    RemoveEdgeButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.P))
                {
                    saveImageButton_Click(null, null);
                }

                if (keyData == (Keys.Control | Keys.M))
                {
                    TrajectoryMatrixButton_Click(null, null);
                }

                if (keyData == (Keys.F1))
                {
                    ProgramHelpButton_Click(null, null);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}