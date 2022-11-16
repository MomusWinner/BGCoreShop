using GameData;

namespace BGCore.Game.Factories
{
    public interface IFactory<TItem> : IBaseFactory
    {
        TItem CreateItem<TConfig>(TConfig config, IContext context);
    }
}