using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ForestGraph;
using GraphService.Dijkstra;
using GUI.Dialog;
using GUI.Drawing;

namespace GUI
{
    public partial class MainForm : Form
    {
        private DrawGraph _drawer;
        private RectangleF _rectangle;
        private bool _rectangleDrawing;
        private ForestGraph<string, VertexData, string, EdgeData> _forestGraph;
        private String _autoloadPath;
        private Point _newWindowPosition;
        private bool _saved;
        private List<string> _graphPath;
        private DijkstraAlhorithm<string, VertexData, string, EdgeData> _dijkstra;

        public MainForm()
        {
            InitializeComponent();
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

            if(File.Exists(_autoloadPath))
            {
                DataManipulator.LoadData(_forestGraph, _autoloadPath);
                graphCanvas.Invalidate();
            }

            _newWindowPosition = GetTopBorderPosition(RemoveEdgeButton);
        }

        private Point GetTopBorderPosition(Control control)
        {
            Point result = control.Location;
            result.X += 2;
            result.Y += 2*control.Size.Height+1;

            Control parent = control.Parent;
            while (!(parent is Form))
            {
                result.X += parent.Location.X;
                result.Y += parent.Location.Y;
                parent = parent.Parent;
            }
            result.X += FindForm().DesktopLocation.X;
            result.Y += FindForm().DesktopLocation.Y;
            return result;
        }

        private void AutoloadSaveButton_Click(object sender, EventArgs e)
        {
            if(DataManipulator.SaveData(_forestGraph, _autoloadPath))
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
            SelectTwoVertexesDialog selectPath = new SelectTwoVertexesDialog(true)
            {
                Location = _newWindowPosition
            };
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectPath.ShowDialog())
                {
                    if(selectPath.StartVertex == selectPath.TargetVertex && _forestGraph.HasVertex(selectPath.TargetVertex))
                    {
                        ShowMessage("Cesta vede do stejného vrcholu!", MessageBoxIcon.Information);
                        continue;
                    }

                    if (!_forestGraph.HasVertex(selectPath.StartVertex) || !_forestGraph.HasVertex(selectPath.TargetVertex))
                    {
                        ShowMessage("Zvolené vrcholy v grafu neexistují!", MessageBoxIcon.Warning);
                        continue;
                    }

                    try
                    {
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
                    repeate = false;
                }
                repeate = false;
            } while (repeate);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            VertexDialog newVertex = new VertexDialog()
            {
                Location = _newWindowPosition
            };
            bool repeate = true;

            do
            {
                if (DialogResult.OK == newVertex.ShowDialog())
                {
                    VertexData data = new VertexData(new PointF(newVertex.X, newVertex.Y), newVertex.Type);
                    _graphPath.Clear();

                    try
                    {
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
                    repeate = false;
                }
                repeate = false;
            } while (repeate);
        }

        private void RemoveVertexButton_Click(object sender, EventArgs e)
        {
            SelectVertexDialog selectVertex = new SelectVertexDialog()
            {
                Location = _newWindowPosition
            };
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectVertex.ShowDialog())
                {
                    try
                    {
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
                    repeate = false;
                }
                repeate = false;
            } while (repeate);
        }

        private void AddEdgeButton_Click(object sender, EventArgs e)
        {
            EdgeDialog newEdge = new EdgeDialog()
            {
                Location = _newWindowPosition
            };
            bool repeate = true;

            do
            {
                if (DialogResult.OK == newEdge.ShowDialog())
                {
                    EdgeData data = new EdgeData(newEdge.Distance, EdgeType.Free);

                    try
                    {
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
                    repeate = false;
                }
                repeate = false;
            } while (repeate);
        }

        private void RemoveEdgeButton_Click(object sender, EventArgs e)
        {
            SelectTwoVertexesDialog selectEdge = new SelectTwoVertexesDialog()
            {
                Location = _newWindowPosition
            };
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectEdge.ShowDialog())
                {
                    try
                    {
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
                    repeate = false;
                }
                repeate = false;
            } while (repeate);
        }

        private void TrajectoryMatrixButton_Click(object sender, EventArgs e)
        {
            TrajectoryMatrixDialog matrixDialog = new TrajectoryMatrixDialog(_forestGraph, _dijkstra);
            matrixDialog.ShowDialog();
        }

        private void ShowMessage(string message, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(message, "Chyba", MessageBoxButtons.OK, icon);
        }

        public void SaveImageManager(Panel canvas)
        {
            SaveFileDialog saveImage = new SaveFileDialog()
            {
                FileName = "Graph_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"),
                Filter = "PNG Image|*.png|JPEG Image|*.jpeg|GIF Image|*.gif|Bitmap Image|*.bmp",
                DefaultExt = "png",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            
            if(saveImage.ShowDialog() == DialogResult.OK)
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
        
        private void saveImageButton_Click(object sender, EventArgs e)
        {
            SaveImageManager(graphCanvas);
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveDataButton_Click(object sender, EventArgs e)
        {
            if(DataManipulator.SaveDataDialog(_forestGraph, true))
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
            _drawer.InitCanvas(graphCanvas.Size, e.Graphics);
            _forestGraph.DrawWithPath(_drawer, _graphPath);

            if (!_rectangle.Location.IsEmpty && !_rectangle.Size.IsEmpty && _rectangleDrawing)
            {
                RectangleF rectangle = new RectangleF(_rectangle.Location, _rectangle.Size);
                _drawer.DrawRectangle(rectangle);
            }
        }

        private void graphCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _rectangleDrawing = true;

                PointF location = e.Location;
                _drawer.Denormalize(ref location);
                _rectangle.Location = location;
            }
            else
            {
                _rectangle.Location = PointF.Empty;
            }
            _rectangle.Size = SizeF.Empty;
        }

        private void graphCanvas_MouseUp(object sender, MouseEventArgs e)
        {
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

            foreach(var edge in edges)
            {
                edge.data.EdgeType = EdgeType.Free;
            }

            var vertexes = _forestGraph.GetAllVertexes();            
            
            foreach (var vertex in vertexes)
            {
                var loc = vertex.data.Location;
                RectangleF rect = _rectangle;

                if (rect.Width < 0)
                {
                    rect.Width = -rect.Width;
                    rect.X -= rect.Width;
                }
                if (_rectangle.Height < 0)
                {
                    rect.Height = -rect.Height;
                    rect.Y -= rect.Height;
                }

                if (loc.X >= rect.Left && loc.X <= rect.Left + rect.Width &&
                    loc.Y >= rect.Top && loc.Y <= rect.Top + rect.Height)
                {
                    var incidentEdges = _forestGraph.GetVertexIncidents(vertex.key);

                    foreach(var edge in incidentEdges)
                    {
                        edge.data.EdgeType = EdgeType.Blocked;
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                if (keyData == (Keys.Control | Keys.S))
                {
                    if(DataManipulator.SaveData(_forestGraph, _autoloadPath))
                    {
                        _saved = false;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void graphCanvas_Resize(object sender, EventArgs e)
        {
            graphCanvas.Invalidate();
            _newWindowPosition = GetTopBorderPosition(RemoveEdgeButton);
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            _newWindowPosition = GetTopBorderPosition(RemoveEdgeButton);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_saved == true)
            {
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Máte neuložené zmìny, chcete je pøed koncem uložit?", "Neuložené zmìny", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DataManipulator.SaveDataDialog(_forestGraph);
            }
        }

        private void AboutProgramButton_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}
