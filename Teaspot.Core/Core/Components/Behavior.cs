namespace Teaspot.Core.Components
{
    public abstract class BehaviorScript : Component
    {
        public BehaviorScript()
        {
        }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
        public virtual void Start() { }

        internal void Init(Window.Window scene)
        {
            scene.OnUpdate += Update;
            scene.FixedUpdate += FixedUpdate;
            scene.LateUpdate += LateUpdate;
            Start();
        }

        public override object Clone()
        {
            throw new NotSupportedException($"Cloning component {typeof(BehaviorScript)} is not supported yet.");
        }
    }
}
