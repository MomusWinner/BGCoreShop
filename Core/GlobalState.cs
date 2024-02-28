using Game.Settings;

namespace Core
{
    public static class GlobalState
    {
        public static bool Initialized { get; set; }
        public static bool InProcess { get; set; }
        public static bool IsGameOver { get; set; }
        public static bool IsGameplay { get; set; }
    }
}