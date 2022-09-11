namespace Core
{
    public static class GlobalEvents
    {
        public static string Start { get; } = GEvent.GetUniqueCategory();
        public static string BothLocationLoaded { get; }= GEvent.GetUniqueCategory();
        public static string BothLocationUnloaded { get; }= GEvent.GetUniqueCategory();
        public static string LocationLoaded { get; }= GEvent.GetUniqueCategory();
        public static string LocationUnloaded { get; }= GEvent.GetUniqueCategory();
        public static string Restart { get; } = GEvent.GetUniqueCategory();
        public static string DropSection { get; }= GEvent.GetUniqueCategory();

        public static string SerializePlayer { get; }= GEvent.GetUniqueCategory();
    }
}