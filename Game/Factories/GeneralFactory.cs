using System;
using System.Collections.Generic;
using System.Linq;
using Game.UI;
using GameData;

namespace BGCore.Game.Factories
{
    public static class GeneralFactory
    {
        private static Dictionary<Type, IBaseFactory> Factories { get; set; }

        public static TItem CreateItem<TItem, TConfig>(TConfig config, IContext context)
        {
            if (Factories is null)
                return default;

            var type = typeof(TItem).IsSubclassOf(typeof(IContext)) ? typeof(IContext) : typeof(TItem);

            return Factories.TryGetValue(type, out var item)
                ? ((IFactory<TItem>)item).CreateItem(config, context)
                : default;
        }

        public static void AddFactory<TItem>(IFactory<TItem> factory)
        {
            Factories ??= new Dictionary<Type, IBaseFactory>();

            if (Factories.ContainsKey(typeof(TItem)))
                Factories[typeof(TItem)] = factory;

            Factories.Add(typeof(TItem), factory);
        }

        public static TFactory GetFactory<TFactory>() where TFactory : IBaseFactory
        {
            return (TFactory)Factories.FirstOrDefault(f => f.Value.GetType() == typeof(TFactory)).Value;
        }
    }
}