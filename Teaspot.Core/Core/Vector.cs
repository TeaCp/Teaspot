using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teaspot.Core
{
    public struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector Right { get { return new Vector(1, 0); } }
        public static Vector Top { get { return new Vector(0, 1); } }
        public static Vector Left { get { return new Vector(-1, 0); } }
        public static Vector Bottom { get { return new Vector(0, -1); } }

        public static Vector operator +(Vector vector) => vector;
        public static Vector operator -(Vector vector) => new Vector(-vector.X, -vector.Y);
        public static Vector operator +(Vector left, Vector right)
        => new Vector(left.X + right.X, left.Y + right.Y);
        public static Vector operator -(Vector left, Vector right)
        => left + (-right);
        public static Vector operator *(int multiplier, Vector vector)
        => new Vector(multiplier * vector.X, multiplier * vector.Y);
        public static Vector operator *(Vector vector, int multiplier)
        => multiplier * vector;

        public int getLength () { return Convert.ToInt32(Math.Sqrt(this.X*this.X + this.Y*this.Y)); }
        public static int getAngleBetween (Vector left, Vector right)
        {
            return Math.Abs(left.X*right.X + left.Y*right.Y)/(left.getLength() * right.getLength());
        }
    }
}
