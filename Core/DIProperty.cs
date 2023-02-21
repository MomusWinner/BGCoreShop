using System;
using System.Collections.Generic;

namespace BGCore.Core
{
    public class DIProperty<T>
    {
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                CallAllActions();
            }
        }

        private T value;
        private Dictionary<Action<T>, Action<T>> actions;
        
        public DIProperty()
        {
            actions = new Dictionary<Action<T>, Action<T>>();
        }

        public void Subscribe(Action<T> action)
        {
            if (!actions.ContainsKey(action))
                actions.Add(action, action);
        }

        public void Unsubscribe(Action<T> action)
        {
            if (actions.ContainsKey(action))
                actions.Remove(action);
        }

        public void UnsubscribeAll()
        {
            actions.Clear();
        }

        private void CallAllActions()
        {
            foreach (var action in actions)
            {
                action.Value.Invoke(value);
            }
        }
    }
}