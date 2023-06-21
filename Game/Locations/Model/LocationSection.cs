using System;
using System.Linq;
using Core.ObjectsSystem;
using GameData;

namespace Core.Locations.Model
{
    public class LocationSection : BaseDroppable
    {
        public Location[] Locations { get; }
        private readonly IContext context;

        public LocationSection(IContext context, IDroppable parent = null, params LocationSetting[] locationSettings) : base(parent)
        {
            this.context = context;
            Locations = new Location[locationSettings.Length];
            for (var i = 0; i < locationSettings.Length; i++)
                Locations[i] = (Location) locationSettings[i].GetInstance(context, null);
            GEvent.Attach(GlobalEvents.Start, OnStart);
        }

        private void OnStart(object[] obj)
        {
            GEvent.Detach(GlobalEvents.Start, OnStart);
            Scheduler.InvokeWhen(() => Locations.All(l => l is {Alive: false}), SetAlive);
        }

        protected override void OnDrop()
        {
            foreach (var loc in Locations)
                loc.Drop();
            base.OnDrop();
        }

        public TDroppable GetObject<TDroppable>(Func<TDroppable, bool> predicate = null) where TDroppable : IDroppable
        {
            foreach (var loc in Locations)
            {
                var result = loc.GetFirstOrDefaultObject(predicate);
                if (result is { })
                    return result;
            }

            return default;
        }
    }
}