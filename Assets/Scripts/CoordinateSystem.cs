
namespace PolarCoordinates
{
    public readonly struct Point
    {
        public readonly float X;
        public readonly float Y;

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point a, Point b)
            => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b)
            => new Point(a.X - b.X, a.Y - b.Y);

        public override string ToString()
            => $"({X}, {Y})";
    }

    public readonly struct GridPoint
    {
        public readonly int X;
        public readonly int Y;

        public GridPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static GridPoint operator +(GridPoint a, GridPoint b)
            => new GridPoint(a.X + b.X, a.Y + b.Y);

        public static GridPoint operator -(GridPoint a, GridPoint b)
            => new GridPoint(a.X - b.X, a.Y - b.Y);

        public override string ToString()
            => $"({X}, {Y})";
    }
}
