using Teaspot.Core.Components;
using Teaspot.Core.Window;

namespace Teaspot.Core
{
    public class GameObject
    {
        public string Name { get; set; }

        private readonly Dictionary<Type, Component> components = new();
        public Dictionary<Type, Component> Components { get { return components; } }

        internal readonly Window.Window scene;

        public GameObject(string name, Window.Window scene)
        {
            Name = name;
            this.scene = scene;
            AddComponent<Transform>();
        }

        /// <summary>
        /// Adds a <see cref="Component"/> of the specified <see cref="Type"/> to this GameObject, if not existing yet.
        /// Simply uses the existing Component otherwise.
        /// </summary>
        /// <returns>A reference to a Component of the specified Type.</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            if (components.TryGetValue(typeof(T), out Component? existing))
            {
                return existing as T;
            }

            T newComponent = (T)Activator.CreateInstance(typeof(T));
            newComponent.GameObject = this;
            components.Add(typeof(T), newComponent);
            return newComponent;
        }
        /// <summary>
        /// Adds a <see cref="Component"/> of the specified <see cref="Type"/> to this GameObject, if not existing yet.
        /// Simply uses the existing Component otherwise.
        /// </summary>
        /// <returns>A reference to a Component of the specified Type.</returns>
        public Component AddComponent(string componentName)
        {
            Type? type = Type.GetType("teaspot.Core.Components." + componentName);
            if (type == null || !type.GetType().IsSubclassOf(typeof(Component)))
            {
                throw new IsNotAComponentException(componentName);
            }

            if (components.TryGetValue(type.GetType(), out Component? existing))
            {
                return existing;
            }

            Component newComponent = Activator.CreateInstance(type) as Component;
            newComponent.GameObject = this;

            components.Add(type, newComponent);
            return newComponent;
        }
        /// <summary>
        /// Adds a <see cref="Component"/> of the specified <see cref="Type"/> to this GameObject, if not existing yet.
        /// Simply uses the existing Component otherwise.
        /// </summary>
        /// <returns>A reference to a Component of the specified Type.</returns>
        public Component AddComponent(Type type)
        {
            if (!type.GetType().IsSubclassOf(typeof(Component)))
            {
                throw new IsNotAComponentException(type.ToString());
            }

            if (components.TryGetValue(type.GetType(), out Component? existing))
            {
                return existing;
            }

            Component newComponent = Activator.CreateInstance(type) as Component;
            newComponent.GameObject = this;
            components.Add(type.GetType(), newComponent);
            return newComponent;
        }
        /// <summary>
        /// Adds a <see cref="Component"/> to this GameObject, if not existing yet.
        /// Simply uses the existing Component otherwise.
        /// </summary>
        /// <returns>A reference to a Component of the specified Type.</returns>
        public Component AddComponent(Component component)
        {
            Type type = component.GetType();

            // Consistency checks. Don't fail silently when we can't do what was intended.
            if (component.GameObject != null) throw new ArgumentException(string.Format(
                "Specified Component '{0}' is already part of another GameObject '{1}'",
                type.GetType(),
                component.GameObject.Name));
            if (components.ContainsKey(type)) throw new InvalidOperationException(string.Format(
                "GameObject '{0}' already has a Component of type '{1}'.",
                Name,
                type.GetType()));

            component.GameObject = this;

            components.Add(type, component);
            return component;
        }

        /// <summary>
        /// Returns a single <see cref="Component"/> that matches the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public T GetComponent<T>() where T : Component
        {
            if (components.TryGetValue(typeof(T), out Component component))
            {
                return component as T;
            }
            throw new ArgumentException(string.Format(
                "Specified Component '{0}' is not a part of the GameObject '{1}'",
                typeof(T),
                Name));
        }
        /// <summary>
        /// Returns a single <see cref="Component"/> that matches the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The Type to match the Components with.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Component GetComponent(Type type)
        {
            if (components.TryGetValue(type, out Component result))
            {
                return result;
            }
            throw new ArgumentException(string.Format(
                "Specified Component '{0}' is not a part of the GameObject '{1}'",
                type.GetType(),
                Name));
        }

        /// <summary>
        /// Gets the single <see cref="Component"/> that matches the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The specified type of Component</typeparam>
        /// <returns><see cref="true"/> if the <see cref="GameObject"/> contains the Component with the specified type; otherwise, <see cref="false"/></returns>
        public bool TryGetComponent<T>(out Component component) where T : Component
        {
            return components.TryGetValue(typeof(T), out component);
        }
        /// <summary>
        /// Gets the single <see cref="Component"/> that matches the specified <see cref="Type"/>.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="GameObject"/> contains the Component with the specified type; otherwise, <see cref="false"/></returns>
        public bool TryGetComponent(Type type, out Component component)
        {
            return components.TryGetValue(type, out component);
        }

    }

    public class IsNotAComponentException : Exception
    {
        public IsNotAComponentException(string typeName) : base($"Type {typeName} is not a Component type.")
        {
        }
    }


}
