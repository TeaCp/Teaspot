

namespace Teaspot.Core.Components
{
    public abstract class Component : ICloneable
    {
        protected GameObject? gameObject;
        public virtual GameObject? GameObject
        {
            get { return gameObject; }
            set
            {
                if (gameObject == null)
                {
                    gameObject = value;
                }
                else
                {
                    if (gameObject != null) throw new ArgumentException(string.Format(
                        "Specified Component '{0}' is already part of another GameObject '{1}'",
                        GetType(),
                        gameObject.Name));
                }
            }
        }
        public virtual bool IsActive { get; set; } = true;

        

        public Component()
        {

        }

        protected Component(Component another)
        {
            gameObject = another.GameObject;
        }

        internal virtual void OnLateUpdate() { }

        /// <summary>
        /// Return the copy of the current <see cref="Component"/>.
        /// </summary>
        /// <returns></returns
        public abstract object Clone();
    }
}
