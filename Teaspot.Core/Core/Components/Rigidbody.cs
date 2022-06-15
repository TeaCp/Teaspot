

namespace Teaspot.Core.Components
{
    public class Rigidbody : Component
    {
        public override GameObject? GameObject
        {
            get => base.GameObject;
            set
            {
                base.GameObject = value;
                transform = value.GetComponent<Transform>();
            }
        }

        public Point Min { get; set; }
        public Point Max { get; set; }
        public bool IsCollisied { get; set; } = false;

        internal Transform? transform;

        public Rigidbody(Point min, Point max)
        {
            this.Min = min;
            this.Max = max;
        }
        public Rigidbody(Rigidbody another)
        {
            this.Min = another.Min;
            this.Max = another.Max;
        }
        public Rigidbody() : this(Point.Zero, Point.Zero)
        { }

        public bool GetCollisionWith(Rigidbody another)
        {
            if (this.Min.X < another.Max.X || this.Max.X > another.Min.X) // Separating Axis Theorem
            {
                return false;
            }
            if (this.Min.Y < another.Max.Y || this.Max.Y > another.Min.Y)
            {
                return false;
            }

            return true;
        }
        public void CheckCollisionWith(Rigidbody another)
        {
            if (this.Max.X + transform.Position.X < another.Min.X + another.transform.Position.X || this.Min.X + transform.Position.X > another.Max.X + another.transform.Position.X) // Separating Axis Theorem
            {
                IsCollisied = false;
                return;
            }
            if (this.Max.Y + transform.Position.Y < another.Min.Y + another.transform.Position.Y || this.Min.Y + transform.Position.Y > another.Max.Y + another.transform.Position.Y)
            {
                IsCollisied = false;
                return;
            }

            IsCollisied = true;
        }

        public override object Clone()
        {
            return new Rigidbody(this);
        }
    }
}
