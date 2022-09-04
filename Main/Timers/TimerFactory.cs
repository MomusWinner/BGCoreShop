using System;
using System.Collections.Generic;
using Core.Main.LoopSystem;

namespace Core.Main.Timers
{
    public class TimerFactory : Singleton<TimerFactory>
    {
        private static readonly List<ITimer> allTimers = new List<ITimer>();
        private static Action UpdateTick { get; set; }
        private static Action FixedUpdateTick { get; set; }
        private static Action LateUpdateTick { get; set; }

        private void Update()
        {
            UpdateTick?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateTick?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdateTick?.Invoke();
        }

        public static ITimer CreateTimer(int updateType, float period, Action<object> onReachedPeriodAction, bool playOnAwake = true)
        {
            var timer = new Timer(period, onReachedPeriodAction, playOnAwake);

            if (updateType == Loops.Update)
            {
                UpdateTick += timer.Execute;
                timer.OnDispose += () =>
                {
                    UpdateTick -= timer.Execute;
                    allTimers.Remove(timer);
                };
            }
            else if(updateType == Loops.LateUpdate)
            {
                LateUpdateTick += timer.Execute;
                timer.OnDispose += () =>
                {
                    LateUpdateTick -= timer.Execute;
                    allTimers.Remove(timer);
                };
            }
            else if (updateType == Loops.FixedUpdate)
            {
                FixedUpdateTick += timer.Execute;
                timer.OnDispose += () =>
                {
                    FixedUpdateTick -= timer.Execute;
                    allTimers.Remove(timer);
                };
            }
            else
            {
                return null;
            }

            allTimers.Add(timer);

            return timer;
        }

        public static void StopAll()
        {
            allTimers.ForEach(t=>t.Stop());
            allTimers.Clear();
        }
        
        public static void PlayALl()
        {
            allTimers.ForEach(t=>t.Play());
        }
        public static void PauseAll()
        {
            allTimers.ForEach(t=>t.Pause());
        }
    }
}