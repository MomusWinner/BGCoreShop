using Core.Entities.Loopables;

namespace Game.Characters.Control
{
    public abstract class BaseCharacterControl : ControlLoopable, ICharacterControl
    {
        protected BaseCharacterControl(string name) : base(name)
        {
        }
    }
}