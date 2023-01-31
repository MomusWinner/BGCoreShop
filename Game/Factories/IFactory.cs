using System;
using Core.ObjectsSystem;
using GameData;

namespace BGCore.Game.Factories
{
    public interface IFactory 
    {
        Type SettingType { get; }
        IDroppable CreateItem<TConfig>(TConfig config, IContext context);
    }
}