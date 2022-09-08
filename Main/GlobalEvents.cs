namespace Core.Main
{
    public static class GlobalEvents
    {
        public static readonly string Start = GEvent.GetUniqueCategory();
        public static readonly string LocationLoaded = GEvent.GetUniqueCategory();
        public static readonly string LocationUnloaded = GEvent.GetUniqueCategory();
        public static readonly string Restart = GEvent.GetUniqueCategory();
        public static readonly string DropSection = GEvent.GetUniqueCategory();

        public static readonly string SerializePlayer = GEvent.GetUniqueCategory();
    }
}