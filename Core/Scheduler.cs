using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.LoopSystem;
using Core.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public static class Scheduler
    {
        private static UnityEngine.MonoBehaviour instance;
        private static Dictionary<Action, ITimer> delayTimers = new Dictionary<Action, ITimer>();
        
        public static void Invoke(Action action, float delay = 0)
        {
            void InvokeAction(object o)
            {
                if (o is ITimer timer)
                {
                    action?.Invoke();
                    delayTimers.Remove(action);
                }
            }

            
            var delayTimer = TimerFactory.CreateTimer(Loops.Update, delay, InvokeAction, true, true);
            delayTimer.SetAlive();
            delayTimers.Add(action, delayTimer);
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

        public static void InvokeWhen(Func<bool> condition, Action action)
        {
            if (!instance)
                instance = new GameObject(nameof(Scheduler)).AddComponent<Mask>();
            instance.StartCoroutine(ConditionUntil(action, condition));
        }

        public static void StopAll()
        {
            foreach (var delayTimer in delayTimers)
            {
                delayTimer.Value.Stop();
                delayTimer.Value.Drop();
            }
            delayTimers.Clear();
        }

        private static IEnumerator ConditionUntil(Action action, Func<bool> condition)
        {
            yield return new WaitUntil(condition.Invoke);
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}