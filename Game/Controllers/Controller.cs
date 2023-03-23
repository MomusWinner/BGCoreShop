using System;
using Core.Entities.Loopables;
using Core.LoopSystem;
using Game.Settings;

namespace Controllers
{
    public abstract class Controller<TValue, TSetting> : ControlLoopable
        where TSetting : BaseSetting
    {
        public Action<TValue> OnStarted { get; set; }
        public Action<TValue> OnUpdated { get; set; }
        public Action<TValue> OnPaused { get; set; }

        protected readonly TSetting setting;
        protected TValue currentValue;
        protected Controller(TSetting setting)
        {
            this.setting = setting;
        }
        
        protected override void OnAlive()
        {
            base.OnAlive();
            LoopOn(Loops.Update, OnUpdate);
        }

        protected override void OnPlay()
        {
            base.OnPlay();
            OnStarted?.Invoke(currentValue);
        }

        protected virtual void OnUpdate()
        {
            OnUpdated?.Invoke(currentValue);
        }

        protected override void OnPause()
        {
            base.OnPause();
            OnPaused?.Invoke(currentValue);
        }
    }
}