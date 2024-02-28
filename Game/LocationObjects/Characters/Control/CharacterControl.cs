using System.Collections.Generic;
using Core.Entities.Loopables;
using Game.Characters.Model;

namespace Game.Characters.Control
{
    public abstract class CharacterControl : ControlLoopable, IReceiver
    {
        protected ICharacter Character { get; }
        private Queue<ICommand> commands = new Queue<ICommand>();

        protected CharacterControl(ICharacter character)
        {
            Character = character;
        }

        public void Pull(ICommand command)
        {
            commands.Enqueue(command);
        }

        public abstract void Action(ICommand command);

        public void ExecuteCommands()
        {
            while (commands.Count > 0)
                commands.Dequeue().Execute(this);
        }
    }
}