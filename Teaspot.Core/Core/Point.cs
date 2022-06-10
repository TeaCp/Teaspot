namespace Teaspot.Core
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y) { this.X = x; this.Y = y; }

        public static Point Zero => new Point(0, 0);

        public static Point operator +(Point point) => point;
        public static Point operator -(Point point) => new Point(-point.X, -point.Y);
        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
        public static Point operator -(Point left, Point right) => left + (-right);
        public static Point operator *(int multiplier, Point point) => 
            new Point(multiplier * point.X, multiplier * point.Y);
        public static Point operator *(Point point, int multiplier) => multiplier * point;
        
        public int getDistanceToOrigin () => Convert.ToInt32(Math.Sqrt(this.X*this.X + this.Y*this.Y));
    }
}