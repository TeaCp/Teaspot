
namespace Teaspot.Core.Components
{
    public class Sprite : Component
    {
        public string? SpritePath { get; set; }

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
