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

        // Create a path to draw the color wheel using FillPie
        Rectangle rect = new Rectangle(0, 0, width, height);
        for (int i = 0; i < 360; i++) // Iterate through 360 degrees for hue
        {
            // Create an inner and outer radial gradient for each segment
            using (Brush brush = new SolidBrush(HsvToRgb(i, 1, 1)))
            {
                g.FillPie(brush, rect, i, 1); // Draw each color segment
            }
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
