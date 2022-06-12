using System.Numerics;
using Raylib_cs;
using Teaspot.Core.Components;

namespace Teaspot.Core.Window
{
    public class Window : IDisposable
    {
        public int Width { get; }
        public int Height { get; }
        public string Title { get; }
        public int TargetFPS { get; }

        public delegate void EventHandler();
        public event EventHandler OnUpdate;
        public event EventHandler LateUpdate;
        public event EventHandler FixedUpdate;

        internal static float UpdateTime => Raylib.GetFrameTime();
        internal static int FixedTime { get; set; } = 500;

        public static bool IsRunning => Raylib.IsWindowReady();

        private List<GameObject> objects = new List<GameObject>();
        private List<Sprite> sprites = new();

        public Window(int width, int height, string title, int targetFPS)
        {
            this.Width = width;
            this.Height = height;
            this.Title = title;
            this.TargetFPS = targetFPS;
            
            OnUpdate += () => { };
            FixedUpdate += () => { };
            LateUpdate += () => { };
        }

        public void Run()
        {
            Raylib.InitWindow(Width, Height, Title);
            Raylib.SetTargetFPS(TargetFPS);

            foreach (GameObject obj in objects)
            {
                foreach (var compPair in obj.Components)
                {
                    if(compPair.Key == typeof(Components.Transform))
                    {
                        Components.Transform transform = compPair.Value as Components.Transform;
                        LateUpdate += transform.LateUpdate;
                    }

                    if (compPair.Key.IsSubclassOf(typeof(BehaviorScript)))
                    {
                        BehaviorScript script = compPair.Value as BehaviorScript;
                        script.Init(this);
                    }

                    if (compPair.Key == typeof(Sprite) || compPair.Key.IsSubclassOf(typeof(Sprite)))
                    {
                        Sprite sprite = compPair.Value as Sprite;
                        sprite.Texture = Raylib.LoadTexture(sprite.SpritePath);
                        sprites.Add(sprite);
                    }
                }
            }

            Thread fixedThread = new Thread((object fixedTime) =>
            {
                while (!Raylib.WindowShouldClose())
                {
                    FixedUpdate.Invoke();
                    Thread.Sleep((int)fixedTime);
                }
            });
            fixedThread.Start(FixedTime);

            while (!Raylib.WindowShouldClose())
            {
                OnUpdate.Invoke();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLUE);
                
                foreach (Sprite sprite in sprites)
                {
                    Raylib.DrawTexturePro(sprite.Texture, sprite.SourceRectangle, sprite.DestRectangle, sprite.Origin, sprite.objTransform.Rotation, Color.RAYWHITE);
                }

                Raylib.EndDrawing();

                LateUpdate.Invoke();
            }
            
            Raylib.CloseWindow();
        }

        public void AddObject(GameObject obj)
        {
            objects.Add(obj);
        }

        public void Dispose()
        {
            
        }

        internal Texture2D LoadTexture(string path)
        {
            return Raylib.LoadTexture(path);
        }
    }
}
