using Teaspot;
using Teaspot.Core.Window;

namespace Teaspot.Core
{
    public static class Time
    {
        /// <summary>
        /// Return time in seconds for last frame drawn
        /// </summary>
        public static float DeltaTime => Window.Window.UpdateTime;
        public static int FixedTime
        {
            get => Window.Window.FixedTime;
            set
            {
                if (Window.Window.IsRunning)
                {
                    throw new NotSupportedException("Cannot change FixedTime value when window already running");
                }
                Window.Window.FixedTime = value;
            }
        }
    }
}
