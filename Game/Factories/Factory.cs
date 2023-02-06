using System;
using System.Collections.Generic;
using System.Linq;
using Core.ObjectsSystem;
using Game.Settings;
using GameData;

namespace BGCore.Game.Factories
{
    public static class Factory
    {
        private static Dictionary<Type, IFactory> Factories { get; set; }

        public static IDroppable CreateItem(BaseSetting config, IContext context)
        {
            return InnerCreate(config, context, Factories);
        }

        public static IDroppable CreateItem(IDroppable parentObject, IContext context)
        {
            var factory = Factories?.FirstOrDefault(e => parentObject.GetType().IsSubclassOf(e.Key));
            return factory?.Value?.CreateItem(parentObject, context);
        }

        public static void AddFactory(IFactory factory)
        {
            Factories ??= new Dictionary<Type, IFactory>();

            if (Factories.ContainsKey(factory.SettingType))
                Factories[factory.SettingType] = factory;

            Factories.Add(factory.SettingType, factory);
        }

        public static TFactory GetFactory<TFactory>() where TFactory : IBaseFactory
        {
            return (TFactory) Factories.FirstOrDefault(f => f.Value.GetType() == typeof(TFactory)).Value;
        }

        private static IDroppable InnerCreate(BaseSetting config, IContext context, IEnumerable<KeyValuePair<Type, IFactory>> proposeFactories)
        {
            var incomeCollection = proposeFactories as KeyValuePair<Type, IFactory>[] ?? proposeFactories.ToArray();
            var factory = incomeCollection.FirstOrDefault(e => config.GetType().IsSubclassOf(e.Key));
            if (factory.Value is null)
                return null;
            var result = factory.Value.CreateItem(config, context);
            if (result is not null)
                return result;
            var outcomeCollection = incomeCollection.Except(new[] {factory});
            return InnerCreate(config, context, outcomeCollection);
        }
    }
}