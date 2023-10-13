using System;

namespace Game.Contexts
{
    public interface IContext
    {
        TType GetContext<TType>(Func<TType, bool> predicate = null) where TType : IContext;
    }
}