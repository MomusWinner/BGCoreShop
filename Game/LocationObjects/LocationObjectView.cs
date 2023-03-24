using BGCore.Game.Settings;
using Contexts;
using Game.LocationObjects;
using GameLogic.Views;
using UnityEngine;

namespace BGCore.Game.LocationObjects
{
    public class LocationObjectView : BaseLocationObjectView<LocationObjectSetting, Transform>, ILocationObject
    {
        public LocationObjectView(LocationObjectSetting setting, IContext context) : base(setting, context)
        {
        }
    }
}