using Core.ObjectsSystem;
using GameData;

namespace BGCore.Game.Factories
{
    public interface IFactory<TItem> : IBaseFactory where TItem : IDroppable
    {
        TItem CreateItem<TConfig>(TConfig config, IContext context);
    }
}