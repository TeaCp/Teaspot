namespace Teaspot.Core
{
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y) { this.X = x; this.Y = y; }

        public static Point Zero => new Point(0f, 0f);

        public static Point operator +(Point point) => point;
        public static Point operator -(Point point) => new Point(-point.X, -point.Y);
        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
        public static Point operator -(Point left, Point right) => left + (-right);
        public static Point operator *(float multiplier, Point point) => 
            new Point(multiplier * point.X, multiplier * point.Y);
        public static Point operator *(Point point, float multiplier) => multiplier * point;
        
        public float getDistanceToOrigin () => Convert.ToInt32(Math.Sqrt(this.X*this.X + this.Y*this.Y));
    }
}