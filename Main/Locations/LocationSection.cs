using System;
using System.Threading;
using Core.Main.Locations;
using Core.Main.ObjectsSystem;
using Submodules.BGLogic.Main.Locations;

namespace Core.Main
{
    public class LocationSection : BaseDroppable
    {
        public static Location StatLocation { get; private set; }
        public static Location DynLocation { get; private set; }

        private readonly LocationSetting statLocationSetting;
        private readonly LocationSetting dynLocationSetting;

        public LocationSection(string name, LocationSetting statLocationSetting, LocationSetting dynLocationSetting) :
            base(name)
        {
            this.statLocationSetting = statLocationSetting;
            this.dynLocationSetting = dynLocationSetting;

            GEvent.AttachOnce(GlobalEvents.Start, OnStart, this);
        }

        private async void OnStart(params object[] obj)
        {
            SetAlive();
            GEvent.Attach(GlobalEvents.DropSection, Drop);
            GEvent.AttachOnce(GlobalEvents.Restart, OnRestart);

            var sLocation = new Location(statLocationSetting);
            var dLocation = new Location(dynLocationSetting);
            (StatLocation, DynLocation) = await LocationLoader.LoadBoth(sLocation, dLocation);
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
                GEvent.Call(GlobalEvents.LocationLoaded, location);
            }
        }
    }
}