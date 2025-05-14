using PaintfromScratch.FiguresClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomBrush;

namespace PaintfromScratch.DrawingClasses
{

    public class EraserTool
    {
        public int Size { get; set; } = 10;
        private Point? lastPoint = null;
        public int Spacing { get; set; } = 5;
        public void ResetLastPoint()
        {
            lastPoint = null;
        }

        public void Erase(Bitmap bitmap, Point location)
        {
            int radius = Size / 2;
            int rSquared = radius * radius;

            Rectangle rect = new Rectangle(
                location.X - radius,
                location.Y - radius,
                Size,
                Size);

            rect.Intersect(new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    int dx = x - location.X;
                    int dy = y - location.Y;
                    if (dx * dx + dy * dy <= rSquared)
                    {
                        bitmap.SetPixel(x, y, Color.Transparent);
                    }
                }
            }
        }

    }

}
