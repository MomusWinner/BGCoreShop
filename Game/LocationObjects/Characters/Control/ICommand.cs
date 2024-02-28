using Core.ObjectsSystem;
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

    public interface IReceiver : IExecutor, IDroppable
    {
        void Pull(ICommand command);
        void Action(ICommand command);
    }
}