using Core.Locations.Model;
using Game.GameData;
using GameLogic.Locations;
using GameLogic.Locations.Settings;

namespace Core.Locations
{
    public static class LocationFactory
    {
        public static Location CreateLocation(LocationSetting locationSetting, BaseData data = null)
        {
            return locationSetting switch
            {
                DynamicLocationSetting dynamicLocationSetting => new DynamicLocation(dynamicLocationSetting, data),
                StaticLocationSetting staticLocationSetting => new StaticLocation(staticLocationSetting, data),
                _ => null
            };
        }
    }
}