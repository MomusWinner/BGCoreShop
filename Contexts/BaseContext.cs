using System;
using System.Collections.Generic;
using System.Linq;

namespace GameData
{
    public abstract class BaseContext : IContext
    {
        private readonly IDictionary<Type, IContext> contexts;

        protected BaseContext()
        {
            contexts = new Dictionary<Type, IContext>();
        }

        public TType GetContext<TType>() where TType : IContext
        {
            return (TType) contexts.FirstOrDefault(c => c.Value is TType).Value;
        }

        public void AddContext<TType>(IContext context) where TType : IContext
        {
            if (context is null)
            {
                return;
            }

            if (contexts.ContainsKey(typeof(TType)))
            {
                contexts[typeof(TType)] = context;
                return;
            }

            contexts.Add(typeof(TType), context);
        }

        public void RemoveContext<TType>(IContext context) where TType : IContext
        {
            if (context is null)
            {
                return;
            }

            contexts.Remove(typeof(TType));
        }
    }
}