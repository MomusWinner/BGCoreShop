using Game.Characters.Model;

namespace Game.Characters.Control
{
    public interface ICommand
    {
        ISignal Signal { get; }
        void Execute(IReceiver receiver);
    }

    public interface IExecutor
    {
        void ExecuteCommands();
    }

    public interface IReceiver : IExecutor
    {
        void Pull(ICommand command);
        void Action(ICommand command);
    }

    public interface ISender : IExecutor
    {
        void Push(ICommand command);
    }
}