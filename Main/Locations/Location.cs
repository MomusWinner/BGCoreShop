using Core.Main.ObjectsSystem;

namespace Core.Main.Locations
{
    public class Location : BaseDroppable
    {
        public string RootSceneName { get; private set; }
        public Location(LocationSetting settings) : base(settings.sceneName)
        {
            RootSceneName = settings.sceneName;
            GEvent.Attach(GlobalEvents.LocationLoaded, Initialize);
        }

        private void Initialize(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.LocationUnloaded, Initialize);
            
        }

        protected override void OnDrop()
        {
        }
    }
}