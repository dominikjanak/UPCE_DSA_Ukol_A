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
        private static Graphics canvas;     // Drawing area
        private static Font drawingFont;    // Font for titles

        private static Size canvasSize;     // Canvas size
        private static Point graphOffset;   // Reduce space form [0,0]
        private static PointF scaleSize;    // Graph resize parameters
        private static bool useNormalize;   // Flag - normalization enabled x disabled

        // Prepare canvas for drawing
        public static void PrepareCanvas(Panel drawingPanel)
        {
            useNormalize = true;
            canvasSize = new Size(drawingPanel.Width, drawingPanel.Height);
            canvas = drawingPanel.CreateGraphics();
            drawingFont = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
        }

        // Clear canvas with white colour
        public static void ClearCanvas() 
        {
            canvas.Clear(Color.White);
        }

        // Calculate graph scaling parameters
        public static void CalculateScales(int xMax, int xMin, int yMax, int yMin)
        {
            graphOffset.X = xMin - 25; // 25 px offset from border
            graphOffset.Y = yMin - 25; // 25 px offset from border

            int graphWidth = xMax - xMin + 50; // 50 px padding
            int graphHeight = yMax - yMin + 50; // 50 px padding

            scaleSize.X = (float)canvasSize.Width / graphWidth;
            scaleSize.Y = (float)canvasSize.Height / graphHeight;
        }

        // Draw edge
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

            // Normalize coordinates
            Normalize(ref start);
            Normalize(ref end);

            canvas.DrawLine(pen, start.X, start.Y, end.X, end.Y);
        }

        // Draw vertex
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

            Normalize(ref vertexLocation); // Normalize coordinates
            DrawFilledCircle(vertexLocation, radius, brush);

            // Draw vertex label
            Point labelPosition = CalculateLabelPosition(vertexLocation, radius, label);
            DrawLabel(label, labelPosition, brush);
        }

        // Recalculate coordinates
        private static Point Normalize(ref Point point)
        {
            if (useNormalize) // if normalization enabled then recalculate point position
            {
                point.X -= graphOffset.X;
                point.Y -= graphOffset.Y;

                point.X = (int)(scaleSize.X * point.X);
                point.Y = (int)(scaleSize.Y * point.Y);
            }

            return point;
        }

        // Calculate coordinates for label 
        private static Point CalculateLabelPosition(Point vertexLocation, int vertexRadius, string vertexLabel, LabelPosition labelPosition = LabelPosition.Top)
        {
            Point labelLocation = new Point(vertexLocation.X, vertexLocation.Y);
            SizeF labelSize;
            
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                labelSize = graphics.MeasureString(vertexLabel, drawingFont);
            }

            FindLabelPlace(ref labelLocation, vertexRadius, labelPosition, labelSize);

            return labelLocation;
        }

        // Calculate coordinates for label around vertex
        private static void FindLabelPlace(ref Point location, int vertexRadius, LabelPosition labelPosition, SizeF labelSize)
        {
            switch(labelPosition)
            {
                case LabelPosition.Top:
                    location.X -= (int)(labelSize.Width / 2);
                    location.Y -= vertexRadius + (int)labelSize.Height;
                    break;
                case LabelPosition.Bottom:
                    location.X -= (int)(labelSize.Width / 2);
                    location.Y += vertexRadius + (int)labelSize.Height;
                    break;
                case LabelPosition.Left:
                    location.X -= (int)(labelSize.Width) + vertexRadius;
                    break;
                case LabelPosition.Right:
                    location.X += (int)(labelSize.Width) + vertexRadius;
                    break;
                case LabelPosition.RightTop:
                    location.X += vertexRadius/2;
                    location.Y -= (int)(labelSize.Height)+2;
                    break;
                case LabelPosition.RightBottom:
                    location.X += vertexRadius/2;
                    location.Y += 2;
                    break;
                case LabelPosition.LeftTop:
                    location.X -= vertexRadius + (int)(labelSize.Width);
                    location.Y -= (int)(labelSize.Height) + 2;
                    break;
                case LabelPosition.LeftBottom:
                    location.X -= vertexRadius + (int)(labelSize.Width);
                    location.Y += 2;
                    break;
            }
        }

        // Draw Label
        private static void DrawLabel(string drawText, Point location, Brush brushColor)
        {
            canvas.DrawString(drawText, drawingFont, brushColor, location.X, location.Y);
        }

        // Draw Filled Circle
        private static void DrawFilledCircle(Point location, float radius, Brush brushColor)
        {
            canvas.FillEllipse(brushColor, location.X - radius, location.Y - radius, radius + radius, radius + radius);
        }

    }
}
