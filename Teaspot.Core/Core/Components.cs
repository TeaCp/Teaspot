﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teaspot.Core
{
    public abstract class Component : ICloneable
    {
        protected GameObject? gameObject;
        public GameObject? GameObject
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

        /// <summary>
        /// Return the copy of the current <see cref="Component"/>.
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

        public Component()
        {

        }

        protected Component(Component another)
        {
            gameObject = another.GameObject;
        }
    }

    public class Transform : Component
    {
        public Vector Position { get; set; }

        public Transform(Vector position)
        {
            Position = position;
        }
        public Transform() : this(new Vector(0, 0))
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