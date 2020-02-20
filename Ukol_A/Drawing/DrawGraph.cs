using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ukol_A.Drawing
{
    class DrawGraph
    {
        private Graphics _canvas;            // Drawing area
        private Font _drawingFontRegular;    // Font for titles
        private Font _drawingFontBold;       // Font for titles

        private SizeF _canvasSize;            // Canvas size
        private PointF _graphOffset;          // Reduce space form [0,0]
        private PointF _scaleRatio;          // Graph resize parameters

        public DrawGraph(Control panel = null)
        {
            if (panel != null)
            {
                InitCanvas(panel);
            }
        }

        // Prepare canvas for drawing
        public void InitCanvas(Control drawingPanel)
        {
            _canvasSize = new SizeF(drawingPanel.Width, drawingPanel.Height);
            _canvas = drawingPanel.CreateGraphics();
            _canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            _drawingFontBold = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            _drawingFontRegular = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        // Calculate graph scaling parameters
        public void SetGraphSize(GraphSize size)
        {
            _graphOffset.X = size.XMin - 31; // 25 px offset from border
            _graphOffset.Y = size.YMin - 30; // 25 px offset from border

            _scaleRatio.X = (float)_canvasSize.Width / (size.XMax - size.XMin + 66); // 50 px padding
            _scaleRatio.Y = (float)_canvasSize.Height / (size.YMax - size.YMin + 71);// 60 px padding
        }

        // Clear canvas with white colour
        public void ClearCanvas()
        {
            _canvas.Clear(Color.White);
        }

        // Draw edge
        public void DrawEdge(PointF start, PointF end, EdgeType edgeType)
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

            _canvas.DrawLine(pen, start.X, start.Y, end.X, end.Y);
        }

        // Draw vertex
        public void DrawVertex(PointF vertexLocation, VertexType vertexType, string label)
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

            string coordinationsLabel = "[" + vertexLocation.X + ";" + vertexLocation.Y + "]";

            Normalize(ref vertexLocation); // Normalize coordinates
            DrawFilledCircle(vertexLocation, radius, brush);

            // Draw vertex label
            PointF labelPosition = CalculateLabelPosition(vertexLocation, radius, label, LabelPosition.Top, true);
            DrawLabel(label, labelPosition, brush, true);

            // Draw vertex coordinations 
            PointF coordinationsPosition = CalculateLabelPosition(vertexLocation, radius, coordinationsLabel, LabelPosition.Bottom);
            brush.Color = Colors.Black;
            DrawLabel(coordinationsLabel, coordinationsPosition, brush);
        }

        public void DrawRectangle(RectangleF rectangle)
        {
            Pen pen = new Pen(Colors.Red, (float)1.3);
            SolidBrush transBrush = new SolidBrush(Color.FromArgb(25, Colors.Red));

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            PointF start = rectangle.Location;
            SizeF size = rectangle.Size;

            if (size.Width < 0)
            {
                size.Width = Math.Abs(size.Width);
                start.X -= size.Width;
            }
            if (size.Height < 0)
            {
                size.Height = Math.Abs(size.Height);
                start.Y -= size.Height;
            }

            Normalize(ref start);
            Normalize(ref size);

            _canvas.FillRectangle(transBrush, start.X, start.Y, size.Width, size.Height);
            _canvas.DrawRectangle(pen, start.X, start.Y, size.Width, size.Height);
        }

        public void Denormalize(ref PointF point)
        {
            point.X /= _scaleRatio.X;
            point.Y /= _scaleRatio.Y;

            point.X += _graphOffset.X;
            point.Y += _graphOffset.Y;
        }

        public void Denormalize(ref SizeF size)
        {
            size.Width /= _scaleRatio.X;
            size.Height /= _scaleRatio.Y;

            size.Width += _graphOffset.X;
            size.Height += _graphOffset.Y;
        }

        // Recalculate coordinates
        public void Normalize(ref PointF point)
        {
            point.X -= _graphOffset.X;
            point.Y -= _graphOffset.Y;

            point.X *= _scaleRatio.X;
            point.Y *= _scaleRatio.Y;
        }

        // Recalculate coordinates
        public void Normalize(ref SizeF size)
        {
            size.Width -= _graphOffset.X;
            size.Height -= _graphOffset.Y;

            size.Width *= _scaleRatio.X;
            size.Height *= _scaleRatio.Y;
        }

        // Calculate coordinates for label 
        private PointF CalculateLabelPosition(PointF vertexLocation, int vertexRadius, string vertexLabel, LabelPosition labelPosition = LabelPosition.Top, bool boldFont = false)
        {
            PointF labelLocation = new PointF(vertexLocation.X, vertexLocation.Y);
            SizeF labelSize;
            
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                if (boldFont)
                {
                    labelSize = graphics.MeasureString(vertexLabel, _drawingFontBold);
                }
                else
                {
                    labelSize = graphics.MeasureString(vertexLabel, _drawingFontRegular);
                }
            }

            FindLabelPlace(ref labelLocation, vertexRadius, labelPosition, labelSize);

            return labelLocation;
        }

        // Calculate coordinates for label around vertex
        private void FindLabelPlace(ref PointF location, int vertexRadius, LabelPosition labelPosition, SizeF labelSize)
        {
            switch(labelPosition)
            {
                case LabelPosition.Top:
                    location.X -= labelSize.Width / 2;
                    location.Y -= vertexRadius + (int)labelSize.Height;
                    break;
                case LabelPosition.Bottom:
                    location.X -= labelSize.Width / 2;
                    location.Y += vertexRadius + 1;
                    break;
                case LabelPosition.Left:
                    location.X -= labelSize.Width + vertexRadius + 3;
                    location.Y -= labelSize.Height / 2;
                    break;
                case LabelPosition.Right:
                    location.X += vertexRadius + 2;
                    location.Y -= labelSize.Height / 2;
                    break;
                case LabelPosition.RightTop:
                    location.X += vertexRadius / 2 + 3;
                    location.Y -= labelSize.Height + 3;
                    break;
                case LabelPosition.RightBottom:
                    location.X += vertexRadius / 2 + 1;
                    location.Y += 4;
                    break;
                case LabelPosition.LeftTop:
                    location.X -= vertexRadius + labelSize.Width;
                    location.Y -= labelSize.Height + 2;
                    break;
                case LabelPosition.LeftBottom:
                    location.X -= vertexRadius + labelSize.Width;
                    location.Y += 2;
                    break;
            }
        }

        // Draw Label
        private void DrawLabel(string drawText, PointF location, Brush brushColor, bool boldFont = false)
        {
            if (boldFont)
            {
                _canvas.DrawString(drawText, _drawingFontBold, brushColor, location.X, location.Y);
            }
            else 
            {
                _canvas.DrawString(drawText, _drawingFontRegular, brushColor, location.X, location.Y);
            }
        }

        // Draw Filled Circle
        private void DrawFilledCircle(PointF location, float radius, Brush brushColor)
        {
            _canvas.FillEllipse(brushColor, location.X - radius, location.Y - radius, radius + radius, radius + radius);
        }
    }
}
