namespace Teaspot.Core.Components
{
    public abstract class BehaviorScript : Component
    {
        public BehaviorScript()
        {
        }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        public virtual void Start() { }
        internal void Init(Window.Window scene)
        {
            scene.OnUpdate += Update;
            scene.FixedUpdate += FixedUpdate;
            Start();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
