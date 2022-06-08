using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teaspot.Core
{
    public struct Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector Right { get { return new Vector(1f, 0f); } }
        public static Vector Top { get { return new Vector(0f, 1f); } }
        public static Vector Left { get { return new Vector(-1f, 0f); } }
        public static Vector Bottom { get { return new Vector(0f, -1f); } }

    }
}
