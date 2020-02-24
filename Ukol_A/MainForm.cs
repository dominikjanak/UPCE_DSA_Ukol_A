using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Ukol_A.Dialog;
using Ukol_A.Drawing;

namespace Ukol_A
{
    public partial class MainForm : Form
    {
        private DrawGraph _drawer;
        private RectangleF _rectangle; // Contains relative data (before graph calculations it must be denormalized)
        private bool _rectangleDrawing;
        ForestGraph<string, VertexData, string, EdgeData> _forestGraph;

        public MainForm()
        {
            InitializeComponent();
            _drawer = new DrawGraph();
            _rectangle = new Rectangle(0, 0, 0, 0);
            _rectangleDrawing = false;
            _forestGraph = new ForestGraph<string, VertexData, string, EdgeData>();

            DataManipulator.InitializeSmall(_forestGraph);
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
                    if (!_forestGraph.AddVertex(newVertex.Key, data))
                    {
                        MessageBox.Show("Vrchol s tímto klíèem již existuje", "Nastala chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SelectVertexDialog selectVertex = new SelectVertexDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectVertex.ShowDialog())
                {
                    string key = selectVertex.Key;
                    if (_forestGraph.RemoveVertex(key) == null)
                    {
                        MessageBox.Show("Vrchol s tímto klíèem neexistuje, nebylo ho tak možné odstranit!", "Nastala chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            EdgeDialog newEdge = new EdgeDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == newEdge.ShowDialog())
                {
                    EdgeData data = new EdgeData(newEdge.Distance, EdgeType.Free);
                    if (!_forestGraph.AddEdge(newEdge.Key, newEdge.StartVertex, newEdge.TargetVertex, data))
                    {
                        MessageBox.Show("Hranu se nepodaøilo vytvoøit. Je pravdìpodovné, je již existuje!", "Nastala chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SelectEdgeDialog selectEdge = new SelectEdgeDialog();
            bool repeate = true;

            do
            {
                if (DialogResult.OK == selectEdge.ShowDialog())
                {
                    if (_forestGraph.RemoveEdge(selectEdge.StartVertex, selectEdge.TargetVertex) == null)
                    {
                        MessageBox.Show("Hrana s tìmito vrcholy neexistuje, nebylo jí tak možné odstranit!", "Nastala chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    graphCanvas.Invalidate();
                    repeate = false;
                }
                repeate = false;
            } while (repeate);
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
                string path = saveImage.FileName;

                int width = canvas.Size.Width;
                int height = canvas.Size.Height;

                Bitmap bitmap = new Bitmap(width, height);
                var drawer = new DrawGraph(bitmap);
                //InitMySmallGraph(drawer);

                bitmap.Save(path);
                Process.Start(path);
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
            DataManipulator.SaveData(_forestGraph);
        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            DataManipulator.LoadData(_forestGraph);
            graphCanvas.Invalidate();
        }

        private void graphCanvas_Paint(object sender, PaintEventArgs e)
        {
            _drawer.InitCanvas(graphCanvas.Size, e.Graphics);
            _forestGraph.Draw(_drawer);

            if (!_rectangle.Location.IsEmpty && !_rectangle.Size.IsEmpty)
            {
                RectangleF rectangle = new RectangleF(_rectangle.Location, _rectangle.Size);
                _drawer.DrawRectangle(rectangle);
            }
        }

        private void graphCanvas_Resize(object sender, EventArgs e)
        {
            graphCanvas.Invalidate();
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
            }
        }

        private void graphCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_rectangleDrawing)
            {
                PointF location = e.Location;
                _drawer.Denormalize(ref location);

                SizeF size = new SizeF(location.X - _rectangle.Location.X, location.Y - _rectangle.Location.Y);
                _rectangle.Size = size;
                graphCanvas.Invalidate();
            }
        }
    }
}
