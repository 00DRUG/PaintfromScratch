using System.Drawing.Drawing2D;

namespace PaintfromScratch.FiguresClasses
{
    public enum ShapeType { None, Rectangle, Ellipse }

    public class Shape
    {
        public ShapeType Type { get; }
        public Rectangle Bounds { get; private set; } // Make setter private to allow modification within the class
        public Color Color { get; set; }
        public float Thickness { get; set; }

        public Shape(ShapeType type, Point start, Point end, Color color, float thickness)
        {
            Type = type;
            Bounds = new Rectangle(
                Math.Min(start.X, end.X),
                Math.Min(start.Y, end.Y),
                Math.Abs(start.X - end.X),
                Math.Abs(start.Y - end.Y));
            Color = color;
            Thickness = thickness;
        }

        public void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, Thickness))
            {
                if (Type == ShapeType.Rectangle)
                    g.DrawRectangle(pen, Bounds);
                else if (Type == ShapeType.Ellipse)
                    g.DrawEllipse(pen, Bounds);
            }
        }

        // Check if a point is inside the shape
        public bool Contains(Point point)
        {
            return Bounds.Contains(point);
        }

        public bool IsNearEdge(Point point, int threshold = 3)
        {
            return Math.Abs(point.X - Bounds.Left) < threshold ||
                   Math.Abs(point.X - Bounds.Right) < threshold ||
                   Math.Abs(point.Y - Bounds.Top) < threshold ||
                   Math.Abs(point.Y - Bounds.Bottom) < threshold;
        }

        // Move the shape by a delta (offset)
        public void Move(int deltaX, int deltaY)
        {
            Bounds = new Rectangle(
                Bounds.X + deltaX,
                Bounds.Y + deltaY,
                Bounds.Width,
                Bounds.Height);
        }

        public void Resize(int deltaX, int deltaY)
        {
            Bounds = new Rectangle(
                Bounds.X,
                Bounds.Y,
                Bounds.Width + deltaX,
                Bounds.Height + deltaY);
        }
        public void DrawBoundingBox(Graphics g)
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                pen.DashStyle = DashStyle.Dash; 
                g.DrawRectangle(pen, Bounds);
            }

            // Draw resize handles (small squares at the corners and edges)
            int handleSize = 8; // Size of the resize handles
            Brush handleBrush = Brushes.White;

            // Top-left handle
            g.FillRectangle(handleBrush, Bounds.Left - handleSize / 2, Bounds.Top - handleSize / 2, handleSize, handleSize);
            g.DrawRectangle(Pens.Black, Bounds.Left - handleSize / 2, Bounds.Top - handleSize / 2, handleSize, handleSize);

            // Top-right handle
            g.FillRectangle(handleBrush, Bounds.Right - handleSize / 2, Bounds.Top - handleSize / 2, handleSize, handleSize);
            g.DrawRectangle(Pens.Black, Bounds.Right - handleSize / 2, Bounds.Top - handleSize / 2, handleSize, handleSize);

            // Bottom-left handle
            g.FillRectangle(handleBrush, Bounds.Left - handleSize / 2, Bounds.Bottom - handleSize / 2, handleSize, handleSize);
            g.DrawRectangle(Pens.Black, Bounds.Left - handleSize / 2, Bounds.Bottom - handleSize / 2, handleSize, handleSize);

            // Bottom-right handle
            g.FillRectangle(handleBrush, Bounds.Right - handleSize / 2, Bounds.Bottom - handleSize / 2, handleSize, handleSize);
            g.DrawRectangle(Pens.Black, Bounds.Right - handleSize / 2, Bounds.Bottom - handleSize / 2, handleSize, handleSize);
        }
    }
}