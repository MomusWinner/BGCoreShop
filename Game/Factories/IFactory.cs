using System;
using Contexts;
using Core.ObjectsSystem;

namespace BGCore.Game.Factories
{
    public interface IFactory 
    {
        Type SettingType { get; }
        IDroppable CreateItem<TConfig>(TConfig config, IContext context);
    }
}