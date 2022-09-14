using Core.Locations.Model;
using Core.Locations.View;
using GameLogic.Locations;
using GameLogic.Locations.Settings;

namespace Game.Locations
{
    public static class LocationViewFactory
    {
        public static LocationView CreateView(Location location)
        {
            return location switch
            {
                DynamicLocation dynamicLocation => new DynamicLocationView(dynamicLocation),
                StaticLocation staticLocation => new StaticLocationView(staticLocation),
                _ => null
            };
        }
    }
}