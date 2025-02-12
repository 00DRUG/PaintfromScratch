using System;
using System.Drawing;

namespace PaintfromScratch
{
    public static class ShapeFactory
    {
        public static Shape CreateShape(ShapeType type, Point start, Point end, Color color, float thickness)
        {
            return new Shape(type, start, end, color, thickness);
        }
    }
}
