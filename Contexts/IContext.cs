using System;

namespace Contexts
{
    public interface IContext
    {
        TType GetContext<TType>(Func<TType, bool> predicate = null) where TType : class, IContext;
        void AddContext<TType>(TType context) where TType : IContext;
        void RemoveContext<TType>() where TType : IContext;
    }
}