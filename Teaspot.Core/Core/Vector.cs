namespace Teaspot.Core
{
    public struct Vector
    {
        public Coordinate Coords { get; set; }

        public Vector (int x, int y) { this.Coords = new Coordinate(x, y); }    
        public Vector (Coordinate coordinate) { this.Coords = coordinate; } 

        public static Vector Right { get => new Vector(1, 0); }
        public static Vector Top { get => new Vector(0, 1); }
        public static Vector Left { get => new Vector(-1, 0); }
        public static Vector Bottom { get => new Vector(0, -1);}

        public static Vector operator +(Vector vector) => vector;
        public static Vector operator -(Vector vector) =>
            new Vector(-vector.Coords.X, -vector.Coords.Y);
        public static Vector operator +(Vector left, Vector right) => 
            new Vector(left.Coords.X + right.Coords.X, left.Coords.Y + right.Coords.Y);
        public static Vector operator -(Vector left, Vector right) => left + (-right);
        public static Vector operator *(int multiplier, Vector vector) => 
            new Vector(multiplier * vector.Coords.X, multiplier * vector.Coords.Y);
        public static Vector operator *(Vector vector, int multiplier) => multiplier * vector;

        public int getLength () => 
            Convert.ToInt32(Math.Sqrt(this.Coords.X*this.Coords.X + this.Coords.Y*this.Coords.Y));
        public static double getAngleBetween (Vector left, Vector right) =>
            Math.Acos(left.Coords.X*right.Coords.X + left.Coords.Y*right.Coords.Y) / (left.getLength() * right.getLength());
    }
}
