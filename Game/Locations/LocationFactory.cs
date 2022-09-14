using Core.Locations.Model;
using Game.GameData;
using GameLogic.Locations;
using GameLogic.Locations.Settings;

namespace Core.Locations
{
    public static class LocationFactory
    {
        public static Location CreateLocation(LocationSetting locationSetting, BaseContext context = null)
        {
            return locationSetting switch
            {
                DynamicLocationSetting dynamicLocationSetting => new DynamicLocation(dynamicLocationSetting, context),
                StaticLocationSetting staticLocationSetting => new StaticLocation(staticLocationSetting, context),
                _ => null
            };
        }
    }
}