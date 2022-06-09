using Raylib_cs;

namespace Teaspot.Core.Window
{
    public class Window : IDisposable
    {
        public int Width { get; }
        public int Height { get; }
        public string Title { get; }
        public int TargetFPS { get; }

        public delegate void UpdateHandler();
        public event UpdateHandler OnUpdate;
        public event UpdateHandler FixedUpdate;

        internal static float UpdateTime => Raylib.GetFrameTime();

        private readonly List<GameObject> objects = new List<GameObject>();
        private readonly Dictionary<Texture2D, Transform> textures = new();

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
                try
                {
                    Sprite sprite = obj.GetComponent<Sprite>();
                    Texture2D texture = Raylib.LoadTexture(sprite.SpritePath);
                    textures.Add(texture, obj.GetComponent<Transform>());
                }
                catch (ArgumentException) { }
            }

            Thread fixedThread = new Thread((object fixedTime) =>
            {
                while (!Raylib.WindowShouldClose())
                {
                    FixedUpdate.Invoke();
                    Thread.Sleep((int)fixedTime);
                }
            });
            fixedThread.Start(500);

            while (!Raylib.WindowShouldClose())
            {
                OnUpdate.Invoke();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLUE);

                foreach (var entry in textures)
                {
                    Raylib.DrawTexture(entry.Key, (int)entry.Value.Position.X, (int)entry.Value.Position.Y, Color.RAYWHITE);
                }

                Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                Raylib.EndDrawing();
            }
            
            Raylib.CloseWindow();
        }

        public void AddObject(GameObject obj)
        {
            if(obj.TryGetComponent<Sprite>(out Component _))
            {
                objects.Add(obj);
            }
            else
            {
                throw new ArgumentException(string.Format(
                "Specified GameObject '{0}' does not have a sprite component",
                obj.Name));
            }
        }

        public void Dispose()
        {
            
        }
    }
}
