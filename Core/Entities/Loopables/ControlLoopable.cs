using Game.Characters.Control;

namespace Core.Entities.Loopables
{
    public abstract class ControlLoopable : Loopable, IControllable
    {
        public bool IsActive { get; private set; }

        protected ControlLoopable(string name) : base(name)
        {
            CallActions = false;
        }

        public virtual void Enable()
        {
            if (IsActive || !Alive)
            {
                return;
            }

            IsActive = true;
            OnEnable();
        }

        public virtual void Disable()
        {
            if (!IsActive || !Alive)
            {
                return;
            }

            IsActive = false;
            Pause();
            OnDisable();
        }

        public virtual void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }

        public void Pause()
        {
            if (!CallActions || !Alive)
            {
                return;
            }

            CallActions = false;
            OnPause();
        }

        public void Play()
        {
            if (CallActions || !Alive)
            {
                return;
            }
            
            CallActions = true;
            OnPlay();
        }

        public override void SetAlive()
        {
            base.SetAlive();
            Enable();
            CallActions = false;
        }

        protected override void OnDrop()
        {
            Disable();
            base.OnDrop();
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnPlay()
        {
        }

        protected virtual void OnPause()
        {
        }
    }
}