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
        private readonly IContext sectionContext;

        public LocationSection(LocationSetting statLocationSetting, LocationSetting dynLocationSetting, IContext context)
        {
            sectionContext = context;
            this.statLocationSetting = statLocationSetting;
            this.dynLocationSetting = dynLocationSetting;

            GEvent.AttachOnce(GlobalEvents.Start, OnStart, this);
        }

#pragma warning disable CS1998
        private async void OnStart(params object[] obj)
#pragma warning restore CS1998
        {
            GEvent.Attach(GlobalEvents.DropSection, Drop);
            GEvent.AttachOnce(GlobalEvents.Restart, OnRestart);

            StatLocation =  (Location) GeneralFactory.CreateItem(statLocationSetting, sectionContext);
            DynLocation = (Location) GeneralFactory.CreateItem(dynLocationSetting, sectionContext);

#if UNITY_WEBGL
            LocationLoader.LoadBoth(StatLocation, DynLocation, SetAlive);

#else
            await LocationLoader.LoadBothAsync(StatLocation, DynLocation);
            SetAlive(null);
#endif
        }

        private void Drop(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.DropSection, Drop);
            base.Drop();
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            StatLocation?.SetAlive(null);
            DynLocation?.SetAlive(null);
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            StatLocation?.Drop();
            DynLocation?.Drop();
        }

        private void OnRestart(params object[] objs)
        {
            if (objs.Length > 0 && objs[0] is Location {Alive: true} location)
            {
                location.Drop();
                location.Refresh();
                location.SetAlive(null);
            }
        }
    }
}