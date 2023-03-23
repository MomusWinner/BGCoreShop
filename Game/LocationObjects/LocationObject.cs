using BGCore.Game.Settings;
using Contexts;
using Game.LocationObjects;

namespace BGCore.Game.LocationObjects
{
    public class LocationObject : BaseLocationObject<LocationObjectView, LocationObjectSetting>
    {
        public LocationObject(LocationObjectSetting setting, IContext context) : base(setting, context)
        {
        }
    }
}