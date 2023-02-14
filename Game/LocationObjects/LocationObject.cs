using BGCore.Game.Settings;
using Game.LocationObjects;
using GameData;

namespace BGCore.Game.LocationObjects
{
    public class LocationObject : BaseLocationObject<LocationObjectView, LocationObjectSetting>
    {
        public LocationObject(LocationObjectSetting setting, IContext context) : base(setting, context)
        {
        }
    }
}