using System;
using Contexts;
using Core.Locations.Model;
using Core.ObjectsSystem;
using GameLogic.Locations;

namespace BGCore.Game.Factories
{
    public class LocationFactory : IFactory
    {
        public Type SettingType => typeof(LocationSetting);
        public IDroppable CreateItem<TConfig>(TConfig config, IContext context)
        {
            return config switch
            {
                SceneLocationSetting setting => new SceneLocation(setting, context),
                _ => null,
            };
        }
    }
}