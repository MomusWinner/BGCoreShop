using System;
using System.Collections.Generic;

namespace BGCore.Core
{
    public abstract class DiProperty<T>
    {
        protected T Value
        {
            get => value;
            set
            {
                this.value = value;
                CallAllActions();
            }
        }

        private T value;
        private readonly Dictionary<Action<T>, Action<T>> actionsForAdd;
        private readonly Dictionary<Action<T>, Action<T>> actions;
        private readonly Dictionary<Action<T>, Action<T>> actionsForRemove;

        protected DiProperty()
        {
            actionsForAdd = new Dictionary<Action<T>, Action<T>>();
            actions = new Dictionary<Action<T>, Action<T>>();
            actionsForRemove = new Dictionary<Action<T>, Action<T>>();
        }

        protected DiProperty(T value)
        {
            this.value = value;
            actionsForAdd = new Dictionary<Action<T>, Action<T>>();
            actions = new Dictionary<Action<T>, Action<T>>();
            actionsForRemove = new Dictionary<Action<T>, Action<T>>();
        }

        public void Subscribe(Action<T> action)
        {
            actionsForAdd.Add(action, action);
        }

        public void Unsubscribe(Action<T> action)
        {
            actionsForRemove.Add(action, action);
        }

        public void UnsubscribeAll()
        {
            actions.Clear();
        }

        private void CallAllActions()
        {
            UpdateActions();
            foreach (var action in actions)
                action.Value.Invoke(value);
        }

        private void UpdateActions()
        {
            foreach (var a in actionsForAdd)
                actions.Add(a.Key, a.Value);
            actionsForAdd.Clear();
            foreach(var a in actionsForRemove)
                actions.Remove(a.Key);
            actionsForRemove.Clear();
        }
    }
}