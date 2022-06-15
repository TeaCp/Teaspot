namespace Teaspot.Core.Components
{
    public abstract class BehaviorScript : Component
    {
        public BehaviorScript()
        {
        }

        internal void ActiveUpdate()
        {
            if (IsActive)
            {
                Update();
            }
        }
        internal void ActiveFixedUpdate()
        {
            if (IsActive)
            {
                FixedUpdate();
            }
        }
        internal void ActiveLateUpdate()
        {
            if (IsActive)
            {
                LateUpdate();
            }
        }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
        public virtual void Start() { }

        internal void Init(Windowing.Window scene)
        {
            scene.OnUpdate += ActiveUpdate;
            scene.FixedUpdate += ActiveFixedUpdate;
            scene.LateUpdate += ActiveLateUpdate;
            Start();
        }

        public override object Clone()
        {
            throw new NotSupportedException($"Cloning component {typeof(BehaviorScript)} is not supported yet.");
        }
    }
}
