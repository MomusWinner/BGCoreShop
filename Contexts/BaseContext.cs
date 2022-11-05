using System;
using System.Collections.Generic;

namespace GameData
{
    public abstract class BaseContext : IContext
    {
        private readonly Dictionary<Type, IContext> contexts;

        protected BaseContext()
        {
            contexts = new Dictionary<Type, IContext>();
        }
        public TType GetContext<TType>() where TType : IContext 
        {
            if (contexts.TryGetValue(typeof(TType), out var value))
            {
                return (TType)value;
            }

            return default;
        }

        public void AddContext(IContext context)
        {
            var type = context.GetType();
            if(contexts.ContainsKey(type))
            {
                contexts[type] = context;
                return;
            }

            contexts.Add(type, context);
        }

        public void RemoveContext(IContext context)
        {
            var type = context.GetType();
            if(contexts.ContainsKey(type))
            {
                contexts[type] = context;
                return;
            }

            contexts.Add(type, context);
        }
    }
}