using System;
using UnityEngine;

namespace Core.Main.Timers
{
    public class Timer : ITimer, IDisposable
    {
        public event Action OnDispose;
        public event Action<TimerArgs> OnTimerTick;
        public float Period { get; private set; }

        public bool IsPlaying { get; private set; }

        private float value;

        private Action<object> onReachedPeriod;

        public Timer(float period, Action<object> onReachedPeriodAction, bool playOnAwake)
        {
            Period = period;
            onReachedPeriod = onReachedPeriodAction;
            
            if (playOnAwake)
            {
                Play();
            }
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
                return;
            }

            value += Time.deltaTime;
            OnTimerTick?.Invoke(new TimerArgs(this, value, Period));
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }

}