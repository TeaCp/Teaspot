using Raylib_cs;
using System.Numerics;


namespace Teaspot.Core.Components
{
    public class Sprite : Component
    {
        protected Texture2D texture;
        protected string? spritePath;
        internal Transform objTransform;


        public Texture2D Texture
        {
            get => texture;
            internal set
            {
                texture = value;
            }
        }
        public string? SpritePath
        {
            get => spritePath;
            set
            {
                objTransform = GameObject.GetComponent<Transform>();
                spritePath = value;
                
            }
        }
        public virtual Rectangle SourceRectangle => new(0f, 0f, texture.width * Math.Sign(objTransform.Scale.X), texture.height * Math.Sign(objTransform.Scale.Y));
        public virtual Rectangle DestRectangle => new(objTransform.Position.X, objTransform.Position.Y, Math.Abs(texture.width * objTransform.Scale.X), Math.Abs(texture.height * objTransform.Scale.Y));
        public virtual Vector2 origin => new(texture.width, texture.height);

        public Sprite()
        {
        }
        public Sprite(Sprite another)
        {
            SpritePath = another.SpritePath;
        }

        public override Sprite Clone()
        {
            return new Sprite(this);
        }
    }
}
