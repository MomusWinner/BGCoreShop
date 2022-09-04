using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Main.LoopSystem;
using Core.Main.Timers;
using UnityEngine;

namespace Core.Main
{
    public class Shoulder : Singleton<Shoulder>
    {
        private static List<ITimer> delayTimers = new List<ITimer>();
        
        public static void Invoke(Action action, float delay = 0)
        {
            void InvokeAction(object o)
            {
                if (o is ITimer timer)
                {
                    action?.Invoke();
                    timer.Dispose();
                    delayTimers.Remove(timer);
                }
            }

            
            var delayTimer = TimerFactory.CreateTimer(Loops.Update, delay, InvokeAction);
            delayTimers.Add(delayTimer);
        }

        public static async void AsyncInvokeWhen(Func<bool> condition, Action action)
        {
            await Task.Run(() =>
            {
                while (!condition.Invoke())
                {

                }
            });
            action.Invoke();
        }

        public void InvokeWhen(Func<bool> condition, Action action)
        {
            StartCoroutine(ConditionUntil(action, condition));
        }

        private IEnumerator ConditionUntil(Action action, Func<bool> condition)
        {
            yield return new WaitUntil(condition.Invoke);
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}