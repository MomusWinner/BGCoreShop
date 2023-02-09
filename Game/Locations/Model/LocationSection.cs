using System;
using System.Linq;
using System.Threading.Tasks;
using BGCore.Game.Factories;
using Core.ObjectsSystem;
using GameData;

namespace Core.Locations.Model
{
    public class LocationSection : BaseDroppable
    {
        public Location[] Locations { get; }
        private readonly IContext context;

        public LocationSection(IContext context, params ViewSetting[] locationSettings)
        {
            this.context = context;
            Locations = new Location[locationSettings.Length];
            for (var i = 0; i < locationSettings.Length; i++)
                Locations[i] = (Location) Factory.CreateItem(locationSettings[i], context);
            GEvent.Attach(GlobalEvents.Start, OnStart);
        }

        private async void OnStart(object[] obj)
        {
            GEvent.Detach(GlobalEvents.Start, OnStart);
            await Task.Run(AwaitLoad);
            SetAlive();
        }

        private void AwaitLoad() { while (Locations.Any(l => l is {Alive: false})) { } }

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