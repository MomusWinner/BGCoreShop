using System;
using System.Collections.Generic;
using System.Linq;
using Core.ObjectsSystem;
using Game.Settings;
using GameData;

namespace BGCore.Game.Factories
{
    public static class GeneralFactory
    {
        private static Dictionary<Type, IFactory> Factories { get; set; }

        public static IDroppable CreateItem(BaseSetting config, IContext context)
        {
            var factory = Factories?.FirstOrDefault(e => config.GetType().IsSubclassOf(e.Key));
            return factory?.Value?.CreateItem(config, context);
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
    }
}