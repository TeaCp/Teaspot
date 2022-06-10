namespace Teaspot.Core
{
    public struct Vector
    {
        public Point Pos { get; set; }

        public Vector (int x, int y) { this.Pos = new Point(x, y); }    
        public Vector (Point position) : this() => new Vector(position.X, position.Y);

        public static Vector UnitRight => new Vector(1, 0);
        public static Vector UnitTop => new Vector(0, 1);
        public static Vector UnitLeft => -UnitRight;
        public static Vector UnitBottom => -UnitTop;

        public static Vector operator +(Vector vector) => vector;
        public static Vector operator -(Vector vector) => new Vector(-vector.Pos);
        public static Vector operator +(Vector left, Vector right) => new Vector(left.Pos + right.Pos);
        public static Vector operator -(Vector left, Vector right) => left + (-right);
        public static Vector operator *(int multiplier, Vector vector) => new Vector(multiplier * vector.Pos);
        public static Vector operator *(Vector vector, int multiplier) => multiplier * vector;

        public int getLength () => this.Pos.getDistanceToOrigin();
        public static double getAngleBetween (Vector left, Vector right) =>
            Math.Acos(left.Pos.X*right.Pos.X + left.Pos.Y*right.Pos.Y) / (left.getLength() * right.getLength());
    }
}
