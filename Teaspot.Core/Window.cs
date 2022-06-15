using System.Numerics;
using Raylib_cs;
using Teaspot.Core.Components;

namespace Teaspot.Core.Windowing
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

        private Scene scene;

        public Window(int width, int height, string title, int targetFPS, Scene scene)
        {
            this.Width = width;
            this.Height = height;
            this.Title = title;
            this.TargetFPS = targetFPS;
            this.scene = scene;

            scene.Window = this;
            
            OnUpdate += () => { };
            FixedUpdate += () => { };
            LateUpdate += () => { };
        }

        public void Run()
        {
            Raylib.InitWindow(Width, Height, Title);
            Raylib.SetTargetFPS(TargetFPS);

            scene.Init();

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
                
                foreach (Sprite sprite in scene.sprites)
                {
                    Raylib.DrawTexturePro(sprite.Texture, sprite.SourceRectangle, sprite.DestRectangle, sprite.Origin, sprite.objTransform.Rotation, Color.RAYWHITE);
                }

                Raylib.EndDrawing();

                LateUpdate.Invoke();
            }
            
            Raylib.CloseWindow();
        }

        public async void LoadSceneAsync(Scene scene)
        {
            scene.Window = this;
            await scene.InitAsync();
            this.scene = scene;
        }
        public void LoadScene(Scene scene)
        {
            scene.Window = this;
            scene.Init();
            this.scene = scene;
        }

        public void Dispose()
        {

        }
    }
}
