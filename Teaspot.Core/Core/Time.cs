using Teaspot;
using Teaspot.Core.Windowing;

namespace Teaspot.Core
{
    public static class Time
    {
        /// <summary>
        /// Return time in seconds for last frame drawn
        /// </summary>
        public static float DeltaTime => Windowing.Window.UpdateTime;
        public static int FixedTime
        {
            get => Windowing.Window.FixedTime;
            set
            {
                if (Windowing.Window.IsRunning)
                {
                    throw new NotSupportedException("Cannot change FixedTime value when window already running");
                }
                Windowing.Window.FixedTime = value;
            }
        }
    }
}
