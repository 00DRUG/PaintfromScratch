using System;
using System.Drawing;

namespace PaintfromScratch
{
    public enum ShapeType { None, Rectangle, Ellipse }

    public class Shape
    {
        public ShapeType Type { get; }
        public Rectangle Bounds { get; }
        public Color Color { get; }
        public float Thickness { get; }

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
    }
}
