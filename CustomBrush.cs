public class CustomBrush
{
    public enum BrushShape { Circle, Square, Triangle, Star }

    public BrushShape Shape { get; set; } = BrushShape.Circle;
    public int Size { get; set; } = 10;
    public Color Color { get; set; } = Color.Black;
    public int Spacing { get; set; } = 1;

    private Point? lastPoint = null;

    public void Draw(Graphics g, Point position)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using (SolidBrush brush = new SolidBrush(Color))
        {
            switch (Shape)
            {
                case BrushShape.Circle:
                    DrawDottedCircle(g, position, brush);
                    break;
                case BrushShape.Square:
                    DrawDottedSquare(g, position, brush);
                    break;
                case BrushShape.Triangle:
                    DrawDottedTriangle(g, position, brush);
                    break;
                case BrushShape.Star:
                    DrawDottedStar(g, position, brush);
                    break;
            }
        }
    }

    // Draw a circle with the spacing effect
    private void DrawDottedCircle(Graphics g, Point position, SolidBrush brush)
    {
        int radius = Size / 2;

        if (lastPoint.HasValue)
        {
            float distance = (float)Math.Sqrt(Math.Pow(position.X - lastPoint.Value.X, 2) + Math.Pow(position.Y - lastPoint.Value.Y, 2));

            if (distance >= Spacing)
            {
                DrawDot(g, position, brush, Size);
                lastPoint = position;
            }
        }
        else
        {
            DrawDot(g, position, brush, Size);
            lastPoint = position;
        }
    }

    private void DrawDottedSquare(Graphics g, Point position, SolidBrush brush)
    {
        if (lastPoint.HasValue)
        {
            float distance = (float)Math.Sqrt(Math.Pow(position.X - lastPoint.Value.X, 2) + Math.Pow(position.Y - lastPoint.Value.Y, 2));
            if (distance >= Spacing)
            {
                DrawDot(g, position, brush, Size);
                lastPoint = position;
            }
        }
        else
        {
            DrawDot(g, position, brush, Size);
            lastPoint = position;
        }
    }

    private void DrawDot(Graphics g, Point position, SolidBrush brush, int dotSize)
    {
        switch (Shape)
        {
            case BrushShape.Circle:
                g.FillEllipse(brush, position.X - dotSize / 2, position.Y - dotSize / 2, dotSize, dotSize);
                break;
            case BrushShape.Square:
                g.FillRectangle(brush, position.X - dotSize / 2, position.Y - dotSize / 2, dotSize, dotSize);
                break;
            case BrushShape.Triangle:
                Point[] trianglePoints = {
                    new Point(position.X, position.Y - dotSize / 2),
                    new Point(position.X - dotSize / 2, position.Y + dotSize / 2),
                    new Point(position.X + dotSize / 2, position.Y + dotSize / 2)
                };
                g.FillPolygon(brush, trianglePoints);
                break;
            case BrushShape.Star:
                PointF[] starPoints = GetStarPoints(position, dotSize);
                g.FillPolygon(brush, starPoints);
                break;
        }
    }

    private void DrawDottedTriangle(Graphics g, Point position, SolidBrush brush)
    {
        if (lastPoint.HasValue)
        {
            float distance = (float)Math.Sqrt(Math.Pow(position.X - lastPoint.Value.X, 2) + Math.Pow(position.Y - lastPoint.Value.Y, 2));
            if (distance >= Spacing)
            {
                DrawDot(g, position, brush, Size);
                lastPoint = position;
            }
        }
        else
        {
            DrawDot(g, position, brush, Size);
            lastPoint = position;
        }
    }

    private void DrawDottedStar(Graphics g, Point position, SolidBrush brush)
    {
        if (lastPoint.HasValue)
        {
            float distance = (float)Math.Sqrt(Math.Pow(position.X - lastPoint.Value.X, 2) + Math.Pow(position.Y - lastPoint.Value.Y, 2));
            if (distance >= Spacing)
            {
                DrawDot(g, position, brush, Size);
                lastPoint = position;
            }
        }
        else
        {
            DrawDot(g, position, brush, Size);
            lastPoint = position;
        }
    }

    private PointF[] GetStarPoints(Point center, int size)
    {
        List<PointF> points = new List<PointF>();
        double angle = -Math.PI / 2;
        for (int i = 0; i < 10; i++)
        {
            double radius = (i % 2 == 0) ? size / 2 : size / 4;
            points.Add(new PointF(
                (float)(center.X + radius * Math.Cos(angle)),
                (float)(center.Y + radius * Math.Sin(angle))
            ));
            angle += Math.PI / 5;
        }
        return points.ToArray();
    }
}
