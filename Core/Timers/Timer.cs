using System;
using Core.Entities.Loopables;
using UnityEngine;

namespace Core.Timers
{
    public class Timer : Loopable, ITimer
    {
        public event Action<TimerArgs> OnTimerTick;
        public float Period { get; private set; }

        public bool IsPlaying { get; private set; }

        private readonly int loop; 
        private float value;

        private bool invokeOnce;

        private Action<object> onReachedPeriod;

        public Timer(int loopType, float period, Action<object> onReachedPeriodAction, bool playOnAwake, bool once = false) : base(null)
        {
            loop = loopType;
            Period = period;
            onReachedPeriod = onReachedPeriodAction;
            invokeOnce = once;
        
            if (playOnAwake)
            {
                Play();
            }

            LoopOn(loopType, Execute, playOnAwake);
        }
        
        public void Play()
        {
            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Stop(bool withTickReached = false)
        {
            Pause();
            
            if (withTickReached)
            {
                onReachedPeriod?.Invoke(this);
            }
            
            value = 0;
        }

        public void AddHandler(Action<object> onReachedPeriodAction)
        {
            onReachedPeriod += onReachedPeriodAction;
        }

        public void RemoveHandler(Action<object> onReachedPeriodAction)
        {
            onReachedPeriod -= onReachedPeriodAction;
        }

        public void SetHandler(Action<object> onReachedPeriodAction)
        {
            onReachedPeriod = onReachedPeriodAction;
        }
        
        public void SetPeriod(float period)
        {
            Period = period;
        }

        public void AddPeriod(float step)
        {
            Period += step;
        }

        public void Execute()
        {
            if (!IsPlaying)
            {
                return;
            }
            
            if (value > Period)
            {
                value = 0;
                onReachedPeriod?.Invoke(this);
                if (invokeOnce)
                {
                    Drop();
                }
                return;
            }

            value += Time.deltaTime;
            OnTimerTick?.Invoke(new TimerArgs(this, value, Period));
        }

        protected override void OnDrop()
        {
            OnTimerTick = null;
            onReachedPeriod = null;
            
            base.OnDrop();
        }
    }

}