using System;
using System.Collections.Generic;
using Core.ObjectsSystem;
using GameData;

namespace BGCore.Game.Factories
{
    public static class GeneralFactory
    {
        private static Dictionary<Type, IBaseFactory> Factories { get; set; }

        public static TItem CreateItem<TItem, TConfig>(TConfig config, IContext context)
            where TItem : IDroppable
        {
            if (Factories is null)
            {
                return default;
            }
            
            return Factories.TryGetValue(typeof(TItem), out var item)
                ? ((IFactory<TItem>) item).CreateItem(config, context)
                : default;
        }

        public static void AddFactory<TItem>(IFactory<TItem> factory) where TItem : IDroppable
        {
            if (Factories is null)
            {
                Factories = new Dictionary<Type, IBaseFactory>();
            }

            if (Factories.ContainsKey(typeof(TItem)))
            {
                Factories[typeof(TItem)] = factory;
            }

            Factories.Add(typeof(TItem), factory);
        }
    }
}