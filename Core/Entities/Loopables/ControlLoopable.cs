using System;

namespace Core.Entities.Loopables
{
    public abstract class ControlLoopable : Loopable, IControllable
    {
        public bool IsActive { get; private set; }

        protected ControlLoopable()
        {
            IsPlaying = false;
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
        
        public void Pause()
        {
            if (!IsPlaying || !Alive)
            {
                return;
            }

            IsPlaying = false;
            OnPause();
        }

        public void Play()
        {
            if (IsPlaying || !Alive)
            {
                return;
            }
            
            IsPlaying = true;
            OnPlay();
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            Enable();
            IsPlaying = false;
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