using System.Collections.Generic;
using Core.ObjectsSystem;
using Game.Characters.Model;

namespace Game.Characters.Control
{
    public abstract class CharacterControl : BaseDroppable, IReceiver
    {
        protected ICharacter Character { get; }
        private Queue<ICommand> commands = new Queue<ICommand>();

        protected CharacterControl(ICharacter character) : base(character.Name)
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
            {
                commands.Dequeue().Execute(this);
            }
        }
    }
}