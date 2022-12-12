using Core.Entities.Loopables;

namespace Game.Characters.Control
{
    public abstract class BaseCharacterControl<TSetting> : ControlLoopable where TSetting : ControllableSetting
    {
        private TSetting setting;
        protected BaseCharacterControl(TSetting setting) : base(setting.name)
        {
            this.setting = setting;
        }
    }
}