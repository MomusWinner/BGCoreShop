using System;
using Contexts;
using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.Settings;
using GameLogic.Locations;

namespace BGCore.Game.Factories
{
    public class LocationFactory : IFactory
    {
        public Type SettingType => typeof(ViewSetting);
        public IDroppable CreateItem<TConfig>(TConfig config, IContext context)
        {
            return config switch
            {
                LocationSetting setting => new SceneLocation(setting, context),
                _ => null,
            };
        }
    }
}