using BGCore.Game.Factories;
using Core.ObjectsSystem;
using GameData;

namespace Core.Locations.Model
{
    public class LocationSection : BaseDroppable
    {
        public Location StatLocation { get; private set; }
        public Location DynLocation { get; private set; }

        private readonly LocationSetting statLocationSetting;
        private readonly LocationSetting dynLocationSetting;
        private IContext sectionContext;

        public LocationSection(string name, LocationSetting statLocationSetting, LocationSetting dynLocationSetting, IContext context) :
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

            StatLocation = GeneralFactory.CreateItem<Location, LocationSetting>(statLocationSetting, sectionContext);
            DynLocation = GeneralFactory.CreateItem<Location, LocationSetting>(dynLocationSetting, sectionContext); 
           
            await LocationLoader.LoadBoth(StatLocation, DynLocation);
            
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
            StatLocation?.Drop();
            DynLocation?.Drop();
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