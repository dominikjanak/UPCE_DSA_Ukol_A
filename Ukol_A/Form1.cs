using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Ukol_A.Drawing;

namespace Ukol_A
{
    public partial class Form1 : Form
    {
        private DrawGraph _drawer;
        private RectangleF _rectangle;

        public Form1()
        {
            InitializeComponent();
            _drawer = new DrawGraph(graphCanvas);
            _rectangle = new Rectangle(0, 0, 0, 0);

            ForestGraph<string, string> gr = new ForestGraph<string, string>();

            /*try
            {
                gr.Draw<string, string>(graphCanvas);
            }
            catch (EmptyGraphException ex)
            {
                MessageBox.Show(ex.Message, "Při vykreslování nastala chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void InstantlyDrawGraph()
        {
            _drawer.ClearCanvas();

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

            _drawer.SetGraphSize(new GraphSize(60, 580, 35, 428));

            _drawer.DrawEdge(crossroads[8], crossroads[9], EdgeType.Free);
            _drawer.DrawEdge(crossroads[8], landings[0], EdgeType.Free);
            _drawer.DrawEdge(crossroads[8], crossroads[1], EdgeType.Free);
            _drawer.DrawEdge(crossroads[1], crossroads[7], EdgeType.Free);
            _drawer.DrawEdge(crossroads[1], crossroads[7], EdgeType.Free);
            _drawer.DrawEdge(crossroads[3], crossroads[2], EdgeType.Free);
            _drawer.DrawEdge(crossroads[2], crossroads[0], EdgeType.Free);
            _drawer.DrawEdge(crossroads[7], crossroads[5], EdgeType.Free);
            _drawer.DrawEdge(crossroads[9], crossroads[5], EdgeType.Free);
            _drawer.DrawEdge(crossroads[5], crossroads[10], EdgeType.Free);
            _drawer.DrawEdge(crossroads[10], crossroads[14], EdgeType.Free);
            _drawer.DrawEdge(crossroads[5], crossroads[11], EdgeType.Free);
            _drawer.DrawEdge(crossroads[2], crossroads[6], EdgeType.Free);
            _drawer.DrawEdge(crossroads[6], crossroads[12], EdgeType.Free);
            _drawer.DrawEdge(crossroads[0], crossroads[6], EdgeType.Free);
            _drawer.DrawEdge(crossroads[0], crossroads[5], EdgeType.Free);
            _drawer.DrawEdge(crossroads[10], crossroads[11], EdgeType.Free);
            _drawer.DrawEdge(crossroads[4], crossroads[3], EdgeType.Blocked);
            _drawer.DrawEdge(crossroads[4], landings[1], EdgeType.Blocked);
            _drawer.DrawEdge(stops[2], crossroads[4], EdgeType.Blocked);
            _drawer.DrawEdge(stops[2], landings[1], EdgeType.Blocked);
            _drawer.DrawEdge(stops[2], crossroads[7], EdgeType.Blocked);
            _drawer.DrawEdge(stops[2], crossroads[1], EdgeType.Blocked);
            _drawer.DrawEdge(landings[0], crossroads[7], EdgeType.Free);
            _drawer.DrawEdge(landings[1], crossroads[7], EdgeType.Free);
            _drawer.DrawEdge(landings[1], crossroads[3], EdgeType.Free);
            _drawer.DrawEdge(stops[0], crossroads[3], EdgeType.Free);
            _drawer.DrawEdge(stops[0], crossroads[2], EdgeType.Free);
            _drawer.DrawEdge(landings[1], crossroads[2], EdgeType.Free);
            _drawer.DrawEdge(landings[0], crossroads[5], EdgeType.Free);
            _drawer.DrawEdge(landings[2], crossroads[0], EdgeType.Free);
            _drawer.DrawEdge(stops[1], crossroads[6], EdgeType.Free);
            _drawer.DrawEdge(stops[3], crossroads[5], EdgeType.Free);
            _drawer.DrawEdge(stops[3], crossroads[10], EdgeType.Free);
            _drawer.DrawEdge(stops[1], crossroads[11], EdgeType.Free);
            _drawer.DrawEdge(landings[2], crossroads[13], EdgeType.Free);
            _drawer.DrawEdge(stops[1], crossroads[14], EdgeType.Free);
            _drawer.DrawEdge(stops[1], crossroads[12], EdgeType.Free);
            _drawer.DrawEdge(stops[1], crossroads[13], EdgeType.Blocked);
            _drawer.DrawEdge(crossroads[0], crossroads[7], EdgeType.Free);

            _drawer.DrawEdge(crossroads[6], landings[2], EdgeType.Free);
            _drawer.DrawEdge(crossroads[11], landings[2], EdgeType.Free);
            _drawer.DrawEdge(crossroads[11], crossroads[14], EdgeType.Free);


            int crossroadsCount = crossroads.Count;
            for (int i = 1; i <= crossroadsCount; i++)
            {
                _drawer.DrawVertex(crossroads[i - 1], VertexType.Crossroads, "K" + i);
            }

            int landingsCount = landings.Count;
            for (int i = 1; i <= landingsCount; i++)
            {
                _drawer.DrawVertex(landings[i - 1], VertexType.Landing, "O" + i);
            }

            int stopsCount = stops.Count;
            for (int i = 1; i <= stopsCount; i++)
            {
                _drawer.DrawVertex(stops[i - 1], VertexType.Stop, "Z" + i);
            }
        }

        public void saveImageManager(Panel canvas)
        {
            SaveFileDialog saveImage = new SaveFileDialog()
            {
                FileName = "Graph_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"),
                Filter = "Bitmap Image (.bmp)|*.bmp|GIF Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|PNG Image (.png)",
                DefaultExt = "png",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            
            if(saveImage.ShowDialog() == DialogResult.OK)
            {
                string path = saveImage.FileName;
                string extension = Path.GetExtension(path).ToLower();
                ImageFormat format = ImageFormat.Png;

                switch (extension)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(extension);
                }

                int width = canvas.Size.Width;
                int height = canvas.Size.Height;

                Bitmap bitmap = new Bitmap(width, height);
                canvas.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));

                bitmap.Save(path, format);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _drawer.InitCanvas(graphCanvas);
            InstantlyDrawGraph();

            if (!_rectangle.Location.IsEmpty && !_rectangle.Size.IsEmpty)
            {
                _drawer.DrawRectangle(_rectangle);
            }
        }

        private void saveImageMenuItem_Click(object sender, EventArgs e)
        {
            saveImageManager(graphCanvas);
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            saveImageManager(graphCanvas);
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void graphCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _rectangle.Location = e.Location;
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
                PointF start = _rectangle.Location;
                SizeF size = new SizeF(e.Location.X - _rectangle.Location.X, e.Location.Y - _rectangle.Location.Y);

                _drawer.Denormalize(ref start);
                _drawer.Denormalize(ref size);

                _rectangle.Location = start;
                _rectangle.Size = size;
            }
            Invalidate();
        }
    }
}
