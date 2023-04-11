using System;
using System.Collections.Concurrent;
using Core.Entities.Loopables;
using Core.LoopSystem;

namespace Game.Networks
{
    public class ThreadDispatcher : ControlLoopable
    {
        private readonly ConcurrentQueue<Action> events;
        
        public ThreadDispatcher()
        {
            events = new ConcurrentQueue<Action>();
        }

        public void AddEvent(Action action) => events.Enqueue(action);
        
        protected override void OnAlive()
        {
            base.OnAlive();
            LoopOn(Loops.Update, OnUpdate);
        }

        private void OnUpdate()
        {
            while (events.Count > 0)
                if(events.TryDequeue(out var action))
                    action?.Invoke();
        }
    }
}