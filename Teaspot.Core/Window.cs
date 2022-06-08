using Raylib_cs;

namespace Teaspot.Core.Window
{
    public class Window : IDisposable
    {
        public int Width { get; }
        public int Height { get; }
        public string Title { get; }

        public delegate void UpdateHandler();
        public event UpdateHandler OnUpdate;
        public event UpdateHandler FixedUpdate;

        internal static float UpdateTime => Raylib.GetFrameTime();


        public Window(int width, int height, string title)
        {
            this.Width = width;
            this.Height = height;
            this.Title = title;

            OnUpdate += () => { };
        }

        public void Run()
        {
            Raylib.InitWindow(Width, Height, Title);
            Raylib.SetTargetFPS(60);

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


                Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                Raylib.EndDrawing();
            }
            
            Raylib.CloseWindow();
        }

        public void Dispose()
        {
            
        }
    }
}
