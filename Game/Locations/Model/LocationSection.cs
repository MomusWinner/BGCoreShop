using System;
using BGCore.Game.Factories;
using Core.ObjectsSystem;
using Game.Settings;
using GameData;
using UnityEngine;

namespace Core.Locations.Model
{
    public class LocationSection : BaseDroppable
    {
        public Location StatLocation { get; private set; }
        public Location DynLocation { get; private set; }

        private readonly LocationSetting statLocationSetting;
        private readonly LocationSetting dynLocationSetting;
        private readonly IContext context;

        public LocationSection(LocationSetting statLocationSetting, LocationSetting dynLocationSetting,
            IContext context)
        {
            this.context = context;
            this.statLocationSetting = statLocationSetting;
            this.dynLocationSetting = dynLocationSetting;

            GEvent.AttachOnce(GlobalEvents.Start, OnStart, this);
        }

        public TDroppable GetObject<TDroppable>(Func<TDroppable, bool> predicate = null)
            where TDroppable : IDroppable
        {
            return StatLocation.GetFirstOrDefaultObject(predicate) ?? DynLocation.GetFirstOrDefaultObject(predicate);
        }

        public T GetConfig<T>() where T : BaseSetting
        {
            return dynLocationSetting.GetConfig<T>();
        }

#pragma warning disable CS1998
        private async void OnStart(params object[] obj)
#pragma warning restore CS1998
        {
            GEvent.Attach(GlobalEvents.DropSection, Drop);
            GEvent.AttachOnce(GlobalEvents.Restart, OnRestart);

            StatLocation = (Location) Factory.CreateItem(statLocationSetting, context);
            DynLocation = (Location) Factory.CreateItem(dynLocationSetting, context);

#if UNITY_WEBGL
            LocationLoader.LoadBoth(StatLocation, DynLocation,()=> SetAlive());
#else
            await LocationLoader.LoadBothAsync(StatLocation, DynLocation);
#endif
            SetAlive(null);
        }

        private void Drop(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.DropSection, Drop);
            base.Drop();
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            StatLocation.SetAlive();
            DynLocation.SetAlive();
            Name += "~" + StatLocation.Name + "~" + DynLocation.Name;
            Debug.Log($"Section {Name} lively");
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            StatLocation?.Drop();
            DynLocation?.Drop();
            Debug.Log($"Section {Name} dropped");
        }

        private void OnRestart(params object[] objs)
        {
            if (objs.Length > 0 && objs[0] is Location {Alive: true} location)
            {
                location.Drop();
                location.Refresh();
                location.SetAlive();
            }
        }
    }
}