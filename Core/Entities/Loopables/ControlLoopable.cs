namespace Core.Entities.Loopables
{
    public abstract class ControlLoopable : Loopable, IControllable
    {
        public bool IsActive { get; private set; }

        protected ControlLoopable()
        {
            CallActions = false;
        }

        public virtual void Enable()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            OnEnable();
        }

        public virtual void Disable()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            Pause();
            OnDisable();
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

        protected override void OnAlive()
        {
            base.OnAlive();
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