using System;
using System.Collections.Generic;

namespace Core.Timers
{
    public static class TimerFactory
    {
        private static readonly List<ITimer> allTimers = new List<ITimer>();
        public static ITimer CreateTimer(int updateType, float period, Action<object> onReachedPeriodAction,
            bool playOnAwake = true, bool invokeOnce = false)
        {
            var timer = new Timer(updateType, period, onReachedPeriodAction, playOnAwake, invokeOnce);

            timer.OnDropped += (d) =>
            {
                allTimers.Remove(timer);
            };

            allTimers.Add(timer);

            return timer;
        }

        public static void StopAll()
        {
            allTimers.ForEach(t => t.Stop());
            allTimers.Clear();
        }

        public static void PlayALl()
        {
            allTimers.ForEach(t => t.Play());
        }

        public static void PauseAll()
        {
            allTimers.ForEach(t => t.Pause());
        }
    }
}