using System;
using System.Collections.Generic;
using System.Linq;

namespace Contexts
{
    public abstract class BaseContext : IContext
    {
        protected readonly IDictionary<Type, IContext> contexts;

        protected BaseContext()
        {
            contexts = new Dictionary<Type, IContext>();
        }

        public TType GetContext<TType>(Func<TType, bool> predicate = null) where TType : class, IContext
        {
            contexts.TryGetValue(typeof(TType), out var context);
            return (TType) context;
        }

        public virtual void AddContext<TType>(TType context) where TType : IContext
        {
            if (context is null)
            {
                return;
            }

            var type = context.GetType();
            
            if (contexts.ContainsKey(type))
            {
                contexts[type] = context;
                return;
            }

            contexts.Add(type, context);
        }

        public void RemoveContext<TType>() where TType : IContext
        {
            contexts.Remove(typeof(TType));
        }
    }
}