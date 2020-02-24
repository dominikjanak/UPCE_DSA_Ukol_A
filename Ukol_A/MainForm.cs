using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
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
            _forestGraph = new ForestGraph<string, VertexData, string, EdgeData>(new GraphItemFactory<string, VertexData, string, EdgeData>());
        }

        private void InitMyBigGraph(ForestGraph<string, VertexData, string, EdgeData> graph)
        {
            List<(int x, int y, VertexType type)> data = new List<(int x, int y, VertexType type)>()
            {
                (182, 305, VertexType.Junction),
                (72, 80, VertexType.Junction),
                (270, 214, VertexType.Junction),
                (446, 324, VertexType.Junction),
                (277, 373, VertexType.Junction),
                (380, 260, VertexType.Junction),
                (338, 455, VertexType.Junction),
                (776, 381, VertexType.Junction),
                (636, 163, VertexType.Junction),
                (483, 544, VertexType.Junction),
                (706, 516, VertexType.Junction),
                (853, 78, VertexType.Junction),
                (729, 194, VertexType.Junction),
                (590, 73, VertexType.Junction),
                (240, 526, VertexType.Junction),
                (50, 389, VertexType.Junction),
                (875, 268, VertexType.Junction),
                (358, 65, VertexType.Junction),
                (934, 184, VertexType.Junction),
                (932, 474, VertexType.Junction),
                (160, 205, VertexType.Junction),
                (136, 555, VertexType.Junction),
                (491, 450, VertexType.Junction),
                (793, 559, VertexType.Junction),
                (729, 444, VertexType.Junction),
                (766, 280, VertexType.Junction),
                (541, 380, VertexType.Junction),
                (549, 210, VertexType.Junction),
                (410, 131, VertexType.Junction),
                (605, 319, VertexType.Junction),
                (591, 500, VertexType.RestArea),
                (701, 347, VertexType.RestArea),
                (510, 281, VertexType.RestArea),
                (140, 388, VertexType.RestArea),
                (442, 205, VertexType.RestArea),
                (515, 120, VertexType.RestArea),
                (170, 104, VertexType.RestArea),
                (845, 167, VertexType.RestArea),
                (661, 248, VertexType.RestArea),
                (889, 383, VertexType.RestArea),
                (377, 515, VertexType.RestArea),
                (832, 479, VertexType.RestArea),
                (623, 424, VertexType.RestArea),
                (723, 103, VertexType.RestArea),
                (294, 123, VertexType.RestArea),
                (70, 211, VertexType.RestArea),
                (87, 483, VertexType.RestArea),
                (217, 456, VertexType.RestArea),
                (411, 390, VertexType.RestArea),
                (302, 303, VertexType.RestArea),
                (180, 35, VertexType.Stop),
                (64, 295, VertexType.Stop),
                (47, 563, VertexType.Stop),
                (442, 32, VertexType.Stop),
                (314, 578, VertexType.Stop),
                (622, 568, VertexType.Stop),
                (936, 566, VertexType.Stop),
                (961, 342, VertexType.Stop),
                (947, 69, VertexType.Stop),
                (754, 42, VertexType.Stop),

            };

            graph.Clear();
            int[] idx = new int[3] { 1, 1, 1 };

            foreach (var vertex in data)
            {
                string key = "";

                switch (vertex.type)
                {
                    case VertexType.Junction:
                        key = "K" + idx[0]++; 
                        break;
                    case VertexType.RestArea:
                        key = "O" + idx[1]++; 
                        break;
                    case VertexType.Stop:
                        key = "Z" + idx[2]++; 
                        break;
                }

                PointF point = new PointF(vertex.x, vertex.y);
                VertexData vertexData = new VertexData(point, vertex.type);

                graph.AddVertex(key, vertexData);
            }

            
        }

        private void InitMySmallGraph(DrawGraph drawer)
        {
            //drawer.ClearCanvas();

            List<Point> crossroads = new List<Point>();
            crossroads.Add(new Point(321, 240));
            crossroads.Add(new Point(90, 153));
            crossroads.Add(new Point(285, 345));
            crossroads.Add(new Point(133, 379));
            crossroads.Add(new Point(69, 346));
            crossroads.Add(new Point(333, 146));
            crossroads.Add(new Point(381, 360));
            crossroads.Add(new Point(195, 207));
            crossroads.Add(new Point(117, 40));
            crossroads.Add(new Point(270, 35));
            crossroads.Add(new Point(460, 100));
            crossroads.Add(new Point(420, 189));
            crossroads.Add(new Point(500, 390));
            crossroads.Add(new Point(500, 283));
            crossroads.Add(new Point(580, 186));

            List<Point> landings = new List<Point>();
            landings.Add(new Point(219, 114));
            landings.Add(new Point(186, 300));
            landings.Add(new Point(405, 246));

            List<Point> stops = new List<Point>();
            stops.Add(new Point(234, 428));
            stops.Add(new Point(570, 296));
            stops.Add(new Point(60, 219));
            stops.Add(new Point(372, 60));

            drawer.SetGraphSize(new GraphSize<float>(60, 580, 35, 428));

            drawer.DrawEdge(crossroads[8], crossroads[9], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[8], landings[0], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[8], crossroads[1], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[1], crossroads[7], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[1], crossroads[7], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[3], crossroads[2], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[2], crossroads[0], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[7], crossroads[5], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[9], crossroads[5], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[5], crossroads[10], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[10], crossroads[14], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[5], crossroads[11], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[2], crossroads[6], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[6], crossroads[12], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[0], crossroads[6], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[0], crossroads[5], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[10], crossroads[11], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[4], crossroads[3], EdgeType.Blocked, "25");
            drawer.DrawEdge(crossroads[4], landings[1], EdgeType.Blocked, "25");
            drawer.DrawEdge(stops[2], crossroads[4], EdgeType.Blocked, "25");
            drawer.DrawEdge(stops[2], landings[1], EdgeType.Blocked, "25");
            drawer.DrawEdge(stops[2], crossroads[7], EdgeType.Blocked, "25");
            drawer.DrawEdge(stops[2], crossroads[1], EdgeType.Blocked, "25");
            drawer.DrawEdge(landings[0], crossroads[7], EdgeType.Free, "25");
            drawer.DrawEdge(landings[1], crossroads[7], EdgeType.Free, "25");
            drawer.DrawEdge(landings[1], crossroads[3], EdgeType.Free, "25");
            drawer.DrawEdge(stops[0], crossroads[3], EdgeType.Free, "25");
            drawer.DrawEdge(stops[0], crossroads[2], EdgeType.Free, "25");
            drawer.DrawEdge(landings[1], crossroads[2], EdgeType.Free, "25");
            drawer.DrawEdge(landings[0], crossroads[5], EdgeType.Free, "25");
            drawer.DrawEdge(landings[2], crossroads[0], EdgeType.Free, "25");
            drawer.DrawEdge(stops[1], crossroads[6], EdgeType.Free, "25");
            drawer.DrawEdge(stops[3], crossroads[5], EdgeType.Free, "25");
            drawer.DrawEdge(stops[3], crossroads[10], EdgeType.Free, "25");
            drawer.DrawEdge(stops[1], crossroads[11], EdgeType.Free, "25");
            drawer.DrawEdge(landings[2], crossroads[13], EdgeType.Free, "25");
            drawer.DrawEdge(stops[1], crossroads[14], EdgeType.Free, "25");
            drawer.DrawEdge(stops[1], crossroads[12], EdgeType.Free, "25");
            drawer.DrawEdge(stops[1], crossroads[13], EdgeType.Blocked, "25");
            drawer.DrawEdge(crossroads[0], crossroads[7], EdgeType.Free, "25");

            drawer.DrawEdge(crossroads[6], landings[2], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[11], landings[2], EdgeType.Free, "25");
            drawer.DrawEdge(crossroads[11], crossroads[14], EdgeType.Free, "25");

            drawer.DrawPath(landings[0], crossroads[5]);
            drawer.DrawPath(crossroads[5], crossroads[11]);
            drawer.DrawPath(crossroads[11], stops[1]);


            int crossroadsCount = crossroads.Count;
            for (int i = 1; i <= crossroadsCount; i++)
            {
                drawer.DrawVertex(crossroads[i - 1], VertexType.Junction, "K" + i);
            }

            int landingsCount = landings.Count;
            for (int i = 1; i <= landingsCount; i++)
            {
                drawer.DrawVertex(landings[i - 1], VertexType.RestArea, "O" + i);
            }

            int stopsCount = stops.Count;
            for (int i = 1; i <= stopsCount; i++)
            {
                drawer.DrawVertex(stops[i - 1], VertexType.Stop, "Z" + i);
            }
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
                InitMySmallGraph(drawer);

                bitmap.Save(path);
                Process.Start(path);
            }
        }

        private void graphCanvas_Paint(object sender, PaintEventArgs e)
        {
            _drawer.InitCanvas(graphCanvas.Size, e.Graphics);
            //InstantlyDrawGraph(_drawer);

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
        
        private void saveImageButton_Click(object sender, EventArgs e)
        {
            SaveImageManager(graphCanvas);
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void BigDataInit_Click(object sender, EventArgs e)
        {
            InitMyBigGraph(_forestGraph);
            graphCanvas.Invalidate();
        }
        
        private void SmallDataInit_Click(object sender, EventArgs e)
        {
            graphCanvas.Invalidate();
            InitMySmallGraph(_drawer);
            graphCanvas.Invalidate();
        }
    }
}
