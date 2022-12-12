using Game.Characters.Control;
using Submodules.BGLogic.Main.Entities;

namespace Core.Entities
{
    public interface IControllable : IPlayable
    {
        bool IsActive { get; }
        void Enable();
        void Disable();
        void ExecuteCommand(ICommand command);
    }
}