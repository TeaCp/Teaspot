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
    }
}
