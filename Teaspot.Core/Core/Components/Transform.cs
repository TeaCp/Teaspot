using Teaspot.Core.Window;


namespace Teaspot.Core.Components
{
    public class Transform : Component
    {
        private Point prevPosition;
        private Point position;

        public Point Position
        {
            get => position;
            set
            {
                prevPosition = position;
                position = value;
            }
        }
        public float Rotation { get; set; } = 0;
        public Point Scale { get; set; } = new(1, 1);
        public Vector Velocity => new Vector(prevPosition, position);

        public override bool IsActive { get => base.IsActive; set { } }

        internal void LateUpdate()
        {
            this.prevPosition = position;
        }

        public Transform(Point position)
        {
            this.position = position;
            prevPosition = position;
            IsActive = true;
        }
        public Transform() : this(Point.Zero)
        {
        }

        protected Transform(Transform another)
        {
            this.position = another.Position;
        }

        public override Transform Clone()
        {
            return new Transform(this);
        }
    }
}
