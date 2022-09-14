using Core.ObjectsSystem;
using Game.GameData;

namespace Core.Locations.Model
{
    public class LocationSection : BaseDroppable
    {
        public Location StatLocation { get; private set; }
        public Location DynLocation { get; private set; }

        private readonly LocationSetting statLocationSetting;
        private readonly LocationSetting dynLocationSetting;
        private BaseContext sectionContext;

        public LocationSection(string name, LocationSetting statLocationSetting, LocationSetting dynLocationSetting, BaseContext context) :
            base(name)
        {
            sectionContext = context;
            this.statLocationSetting = statLocationSetting;
            this.dynLocationSetting = dynLocationSetting;

            GEvent.AttachOnce(GlobalEvents.Start, OnStart, this);
        }

        private async void OnStart(params object[] obj)
        {
            GEvent.Attach(GlobalEvents.DropSection, Drop);
            GEvent.AttachOnce(GlobalEvents.Restart, OnRestart);

            var sLocation = LocationFactory.CreateLocation(statLocationSetting, sectionContext);
            var dLocation = LocationFactory.CreateLocation(dynLocationSetting, sectionContext);
            (StatLocation, DynLocation) = await LocationLoader.LoadBoth(sLocation, dLocation);
            
            GEvent.Call(GlobalEvents.LocationScenesLoaded);
            SetAlive();
        }

        private void Drop(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.DropSection, Drop);
            base.Drop();
        }

        protected override void OnDrop()
        {
            LocationLoader.DropBoth(StatLocation, DynLocation);
        }

        private void OnRestart(params object[] objs)
        {
            if (objs.Length > 0 && objs[0] is Location {Alive: true} location)
            {
                location.Drop();
                location.Refresh();
            }
        }
    }
}