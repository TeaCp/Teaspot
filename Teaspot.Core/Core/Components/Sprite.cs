
namespace Teaspot.Core.Components
{
    public class Sprite : Component
    {
        public string? TexturePath { get; set; }

        public Sprite()
        {
        }
        public Sprite(Sprite another)
        {
            TexturePath = another.TexturePath;
        }

        public override Sprite Clone()
        {
            return new Sprite(this);
        }
    }
}
