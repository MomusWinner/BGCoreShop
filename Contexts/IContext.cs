namespace GameData
{
    public interface IContext
    {
        TType GetContext<TType>() where TType : IContext;
        void AddContext<TType>(IContext context) where TType : IContext;
    }
}