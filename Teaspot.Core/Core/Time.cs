using Teaspot;
using Teaspot.Core.Window;

namespace Teaspot.Core
{
    public static class Time
    {
        public static float DeltaTime => Window.Window.UpdateTime;
    }
}
