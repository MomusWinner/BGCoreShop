using System;

namespace Core.Timers
{
    public interface ITimer 
    {
        event Action<TimerArgs> OnTimerTick;
        float Period { get; }
        bool IsPlaying { get; }
        void Play();
        void Pause();
        void Stop(bool withTickReached = false);

        void AddHandler(Action<object> onTickAction);
        void RemoveHandler(Action<object> onTickAction);
        void SetHandler(Action<object> onTickAction);
        void SetPeriod(float period);
        void AddPeriod(float step);
    }
}