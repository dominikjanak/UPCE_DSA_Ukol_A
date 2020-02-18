using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Ukol_A.DataStructures.Enums;

namespace Ukol_A.Drawing
{
    static class DrawGraph
    {
        private static Graphics canvas;
        private static Font drawingFont;

        public static void PrepareCanvas(Panel drawingPanel) 
        {
            canvas = drawingPanel.CreateGraphics();
            drawingFont = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
        }

        public static void ClearCanvas() 
        {
            canvas.Clear(Color.White);
        }

        public static void DrawEdge(Point start, Point end, EdgeType edgeType)
        {
            Pen pen = new Pen(Color.Transparent, 2);

            switch (edgeType)
            {
                case EdgeType.Free:
                    pen.Color = Colors.Gray;
                    break;
                case EdgeType.Blocked:
                    pen.Color = Colors.Red;
                    break;
            }

            canvas.DrawLine(pen, start.X, start.Y, end.X, end.Y);
        }

        public static void DrawVertex(Point vertexLocation, VertexType vertexType, string label)
        {
            int radius = 0;
            SolidBrush brush = new SolidBrush(Color.Transparent);

            switch (vertexType)
            {
                case VertexType.Crossroads:
                    radius = 5;
                    brush = new SolidBrush(Colors.Brown);
                    break;
                case VertexType.Landing:
                    radius = 7;
                    brush = new SolidBrush(Colors.Blue);
                    break;
                case VertexType.Stop:
                    radius = 9;
                    brush = new SolidBrush(Colors.Green);
                    break;
            }

            DrawFilledCircle(vertexLocation, radius, brush);

            Point labelPosition = CalculateLabelPosition(vertexLocation, radius, label);

            DrawLabel(label, labelPosition, brush);


        }

        private static Point CalculateLabelPosition(Point vertexLocation, int vertexRadius, string vertexLabel)
        {
            Point labelPosition = new Point(vertexLocation.X, vertexLocation.Y);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString(vertexLabel, drawingFont);
                labelPosition.X -= (int)(size.Width / 2);
                labelPosition.Y -= vertexRadius + (int)size.Height;
            }

            return labelPosition;
        }

        private static void DrawLabel(string drawText, Point location, Brush brushColor)
        {
            canvas.DrawString(drawText, drawingFont, brushColor, location.X, location.Y);
        }

        private static void DrawFilledCircle(Point location, float radius, Brush brushColor)
        {
            canvas.FillEllipse(brushColor, location.X - radius, location.Y - radius, radius + radius, radius + radius);
        }

    }
}
