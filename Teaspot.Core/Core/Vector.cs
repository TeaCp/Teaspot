namespace Teaspot.Core
{
    public struct Vector
    {
        public Point Pos { get; set; }

        public Vector (float x, float y) { this.Pos = new Point(x, y); }    
        public Vector (Point endOfRadiusVector) : this(endOfRadiusVector.X, endOfRadiusVector.Y) { }
        public Vector (Point begin, Point end) : this(end - begin) { }

        public static Vector UnitRight => new Vector(1f, 0f);
        public static Vector UnitTop => new Vector(0f, 1f);
        public static Vector UnitLeft => -UnitRight;
        public static Vector UnitBottom => -UnitTop;

        public static Vector operator +(Vector vector) => vector;
        public static Vector operator -(Vector vector) => new Vector(-vector.Pos);
        public static Vector operator +(Vector left, Vector right) => new Vector(left.Pos + right.Pos);
        public static Vector operator -(Vector left, Vector right) => left + (-right);
        public static Vector operator *(float multiplier, Vector vector) => new Vector(multiplier * vector.Pos);
        public static Vector operator *(Vector vector, float multiplier) => multiplier * vector;
        public static bool operator ==(Vector left, Vector right) => left.Pos == right.Pos;
        public static bool operator !=(Vector left, Vector right) => left.Pos != right.Pos;

        public double getLength () => this.Pos.getDistanceToOrigin();
        public static double getScalarProduction (Vector left, Vector right) => left.Pos.X*right.Pos.X + left.Pos.Y*right.Pos.Y;
        public static double getAngleBetween (Vector left, Vector right) =>
            Math.Acos(getScalarProduction(left, right) / (left.getLength() * right.getLength()));

        public static Vector Zero => new Vector(0, 0);
        public static Vector Right => new Vector(1, 0);
        public static Vector Left => new Vector(-1, 0);
        public static Vector Top => new Vector(0, 1);
        public static Vector Bottom => new Vector(0, -1);

    }
}
