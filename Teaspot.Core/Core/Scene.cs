using Teaspot.Core.Components;
using Teaspot.Core.Windowing;

using Raylib_cs;

namespace Teaspot.Core
{
    public class Scene
    {
        private Window? window;
        internal List<GameObject> objects = new List<GameObject>();
        internal List<Sprite> sprites = new();

        public string Name { get; set; }
        public Window? Window
        {   get => window;
            set => window = value;
        }

        public int WindowTargetFPS => window.TargetFPS;

        public Scene(string name)
        {
            this.Name = name;
        }

        internal void Init()
        {
            if (window == null)
            {
                throw new NullReferenceException("Window value is null");
            }

            foreach (GameObject obj in objects)
            {
                foreach (var compPair in obj.Components)
                {
                    window.LateUpdate += compPair.Value.OnLateUpdate;

                    if (compPair.Key.IsSubclassOf(typeof(BehaviorScript)))
                    {
                        BehaviorScript script = compPair.Value as BehaviorScript;
                        script.Init(window);
                    }

                    if (compPair.Key == typeof(Sprite) || compPair.Key.IsSubclassOf(typeof(Sprite)))
                    {
                        Sprite sprite = compPair.Value as Sprite;
                        sprite.Texture = Raylib.LoadTexture(sprite.SpritePath);
                        sprites.Add(sprite);
                    }
                }
            }
        }

        internal async Task InitAsync()
        {
            await Task.Run(() => Init());
        }

        public void AddObject(GameObject obj)
        {
            obj.scene = this;
            objects.Add(obj);
        }

    }
}
