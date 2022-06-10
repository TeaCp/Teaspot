
namespace Teaspot.Core.Components
{
    public class Transform : Component
    {
        public Point Position { get; set; }
        public float Rotation { get; set; } = 0;
        public Point Scale { get; set; } = new(1, 1);


        public Transform(Point position)
        {
            Position = position;
        }
        public Transform() : this(Point.Zero)
        {
        }

        protected Transform(Transform another)
        {
            Position = another.Position;
        }

        public override Transform Clone()
        {
            return new Transform(this);
        }
    }
}
