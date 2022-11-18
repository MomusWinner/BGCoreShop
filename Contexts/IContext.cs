namespace GameData
{
    public interface IContext
    {
        TType GetContext<TType>() where TType : class, IContext;
        void AddContext<TType>(TType context) where TType : IContext;
    }
}