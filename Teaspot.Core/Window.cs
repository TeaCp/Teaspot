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
        public event EventHandler FixedUpdate;

        internal static float UpdateTime => Raylib.GetFrameTime();
        internal static float FixedTime
        {
            get => Raylib.GetFrameTime();
            set { }
        }

        public static bool IsRunning => Raylib.IsWindowReady();

        private readonly List<GameObject> objects = new List<GameObject>();
        private readonly Dictionary<Texture2D, Components.Transform> textures = new();

        public Window(int width, int height, string title, int targetFPS)
        {
            this.Width = width;
            this.Height = height;
            this.Title = title;
            this.TargetFPS = targetFPS;

            OnUpdate += () => { };
            FixedUpdate += () => { };
        }

        public void Run()
        {
            Raylib.InitWindow(Width, Height, Title);
            Raylib.SetTargetFPS(TargetFPS);

            foreach (GameObject obj in objects)
            {
                foreach (var compPair in obj.Components)
                {
                    if (compPair.Key.IsSubclassOf(typeof(BehaviorScript)))
                    {
                        BehaviorScript script = compPair.Value as BehaviorScript;
                        script.Init(this);
                    }

                    if (compPair.Key == typeof(Sprite))
                    {
                        Sprite sprite = compPair.Value as Sprite;
                        Texture2D texture = Raylib.LoadTexture(sprite.SpritePath);
                        textures.Add(texture, obj.GetComponent<Components.Transform>());
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

                foreach (var entry in textures)
                {
                    Rectangle sourceRec = new(0f,0f,entry.Key.width,entry.Key.height);
                    Rectangle destRec = new(entry.Value.Position.X, entry.Value.Position.Y, entry.Key.width, entry.Key.height);
                    Vector2 origin = new(entry.Key.width * entry.Value.Scale.X, entry.Key.height * entry.Value.Scale.Y);

                    Raylib.DrawTexturePro(entry.Key, sourceRec, destRec, origin, entry.Value.Rotation, Color.RAYWHITE);
                }

                Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                Raylib.EndDrawing();
            }
            
            Raylib.CloseWindow();
        }

        public void AddObject(GameObject obj)
        {
            objects.Add(obj);
            //if(obj.TryGetComponent<Sprite>(out Component _))
            //{
            //    objects.Add(obj);
            //}
            //else
            //{
            //    throw new ArgumentException(string.Format(
            //    "Specified GameObject '{0}' does not have a sprite component",
            //    obj.Name));
            //}
        }

        public void Dispose()
        {
            
        }
    }
}
