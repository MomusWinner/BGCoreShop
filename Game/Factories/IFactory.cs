using GameData;

namespace BGCore.Game.Factories
{
    public interface IFactory<out TItem> : IBaseFactory
    {
        TItem CreateItem<TConfig>(TConfig config, IContext context);
    }
}