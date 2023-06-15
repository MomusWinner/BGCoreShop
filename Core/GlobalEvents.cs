namespace Core
{
    public static class GlobalEvents
    {
        public static string Start { get; } = GEvent.GetUniqueCategory();
        public static string LocationScenesUnloaded { get; }= GEvent.GetUniqueCategory();
        public static string LocationViewLoaded { get; }= GEvent.GetUniqueCategory();
        public static string LocationViewUnloaded { get; }= GEvent.GetUniqueCategory();
        public static string Restart { get; } = GEvent.GetUniqueCategory();
        public static string DropSection { get; }= GEvent.GetUniqueCategory();

        public static string SerializePlayer { get; }= GEvent.GetUniqueCategory();
    }

    public static class AREvents
    {
        public static string ChangeInfoState { get; } = GEvent.GetUniqueCategory();
        public static string LimitedState { get; } = GEvent.GetUniqueCategory();
        public static string TrackedState { get; } = GEvent.GetUniqueCategory();
        public static string SearchState { get; } = GEvent.GetUniqueCategory();
    }
}