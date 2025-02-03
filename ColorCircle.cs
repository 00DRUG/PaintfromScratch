using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ColorCircle : Control
{
    public event EventHandler<Color> ColorSelected; // Event when color is picked

    public ColorCircle()
    {
        this.DoubleBuffered = true; // Reduces flickering
        this.Size = new Size(200, 200); // Default size
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawColorWheel(e.Graphics);
    }

    private void DrawColorWheel(Graphics g)
    {
        int width = this.Width;
        int height = this.Height;
        int radius = Math.Min(width, height) / 2;

        using (Bitmap bmp = new Bitmap(width, height))
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double angle = Math.Atan2(y - height / 2, x - width / 2) * 180 / Math.PI;
                    double distance = Math.Sqrt(Math.Pow(x - width / 2, 2) + Math.Pow(y - height / 2, 2));
                    double maxRadius = radius;

                    if (distance <= maxRadius)
                    {
                        Color color = HsvToRgb(angle, distance / maxRadius, 1);
                        bmp.SetPixel(x, y, color);
                    }
                }
            }
            g.DrawImage(bmp, 0, 0);
        }
    }

    private Color HsvToRgb(double h, double s, double v)
    {
        double c = v * s;
        double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
        double m = v - c;
        double r = 0, g = 0, b = 0;

        if (h >= 0 && h < 60) { r = c; g = x; }
        else if (h >= 60 && h < 120) { r = x; g = c; }
        else if (h >= 120 && h < 180) { g = c; b = x; }
        else if (h >= 180 && h < 240) { g = x; b = c; }
        else if (h >= 240 && h < 300) { r = x; b = c; }
        else if (h >= 300 && h < 360) { r = c; b = x; }

        return Color.FromArgb((int)((r + m) * 255), (int)((g + m) * 255), (int)((b + m) * 255));
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        int centerX = Width / 2;
        int centerY = Height / 2;
        double distance = Math.Sqrt(Math.Pow(e.X - centerX, 2) + Math.Pow(e.Y - centerY, 2));

        if (distance <= centerX) // Inside the circle
        {
            double angle = Math.Atan2(e.Y - centerY, e.X - centerX) * 180 / Math.PI;
            if (angle < 0) angle += 360;
            Color selectedColor = HsvToRgb(angle, distance / centerX, 1);
            ColorSelected?.Invoke(this, selectedColor);
        }
    }
}
