namespace Core
{
    public static class GlobalEvents
    {
        public static string Start { get; } = GEvent.GetUniqueCategory();
        public static string LocationScenesUnloaded { get; }= GEvent.GetUniqueCategory();
        public static string Restart { get; } = GEvent.GetUniqueCategory();
        public static string DropSection { get; }= GEvent.GetUniqueCategory();
    }
}