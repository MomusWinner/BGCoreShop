using System;

namespace GameData
{
    public interface IContext
    {
        TType GetContext<TType>() where TType : IContext;
    }
}