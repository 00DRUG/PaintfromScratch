public class CustomBrush
{
    public enum BrushShape { Circle, Square, Triangle, Star }

    public BrushShape Shape { get; set; } = BrushShape.Circle;
    public int Size { get; set; } = 10;
    public Color Color { get; set; } = Color.Black;
    public int Spacing { get; set; } = 5;

    private Point? lastPoint = null;
    public void ResetLastPoint()
    {
        lastPoint = null;
    }

    public void Draw(Graphics g, Point position)
    {
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
        float radius = Size ;
        if (!lastPoint.HasValue)
        {
            DrawDot(g, position, brush, Size);
            lastPoint = position;
            return;
        }
        Point prev = lastPoint.Value;
        float dx = position.X - prev.X;
        float dy = position.Y - prev.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        if (distance >= Spacing)
        {
            int steps = (int)(distance / Spacing);
            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                int x = (int)(prev.X + t * dx);
                int y = (int)(prev.Y + t * dy);
                DrawDot(g, new Point(x, y), brush, Size);
            }

            lastPoint = position;
        }

    }
    // Draw a square with the spacing effect
    private void DrawDottedSquare(Graphics g, Point position, SolidBrush brush)
    {
        int dotSize = Size ;
        if (!lastPoint.HasValue)
        {
            DrawDot(g, position, brush, dotSize);
            lastPoint = position;
            return;
        }

        Point prev = lastPoint.Value;
        float dx = position.X - prev.X;
        float dy = position.Y - prev.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        if (distance >= Spacing)
        {
            int steps = (int)(distance / Spacing);
            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                int x = (int)(prev.X + t * dx);
                int y = (int)(prev.Y + t * dy);
                DrawDot(g, new Point(x, y), brush, dotSize);
            }
            lastPoint = position;
        }
    }

    private void DrawDottedTriangle(Graphics g, Point position, SolidBrush brush)
    {
        int dotSize = Size ;
        if (!lastPoint.HasValue)
        {
            DrawDot(g, position, brush, dotSize);
            lastPoint = position;
            return;
        }

        Point prev = lastPoint.Value;
        float dx = position.X - prev.X;
        float dy = position.Y - prev.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        if (distance >= Spacing)
        {
            int steps = (int)(distance / Spacing);
            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                int x = (int)(prev.X + t * dx);
                int y = (int)(prev.Y + t * dy);
                DrawDot(g, new Point(x, y), brush, dotSize);
            }
            lastPoint = position;
        }
    }

    private void DrawDottedStar(Graphics g, Point position, SolidBrush brush)
    {
        int dotSize = Size ;
        if (!lastPoint.HasValue)
        {
            DrawDot(g, position, brush, dotSize);
            lastPoint = position;
            return;
        }

        Point prev = lastPoint.Value;
        float dx = position.X - prev.X;
        float dy = position.Y - prev.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        if (distance >= Spacing)
        {
            int steps = (int)(distance / Spacing);
            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                int x = (int)(prev.X + t * dx);
                int y = (int)(prev.Y + t * dy);
                DrawDot(g, new Point(x, y), brush, dotSize);
            }
            lastPoint = position;
        }
    }

    // Drawing a dot with the current brush size
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
   
    // Helper function to generate points for the star
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
