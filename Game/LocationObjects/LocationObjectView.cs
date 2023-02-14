using BGCore.Game.Settings;
using GameLogic.Views;
using UnityEngine;

namespace BGCore.Game.LocationObjects
{
    public class LocationObjectView : BaseLocationObjectView<LocationObjectSetting, Transform>
    {
        public LocationObjectView(LocationObjectSetting setting) : base(setting)
        {
        }
    }
}