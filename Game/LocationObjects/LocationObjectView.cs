using BGCore.Game.Settings;
using Contexts;
using GameLogic.Views;
using UnityEngine;

namespace BGCore.Game.LocationObjects
{
    public class LocationObjectView : BaseLocationObjectView<LocationObjectSetting, Transform>
    {
        public LocationObjectView(LocationObjectSetting setting, IContext context) : base(setting, context)
        {
        }
    }
}