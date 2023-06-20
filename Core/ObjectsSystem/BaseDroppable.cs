using System;
using Core.Locations.Model;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; protected set; }
        public event Action<IDroppable> Dropped;

        protected IDroppable parent;

        protected BaseDroppable()
        {
            Name = GetType().Name;
        }

        public void SetAlive(IDroppable parent = null)
        {
            this.parent = parent;
            Alive = true;
            OnAlive();
        }

        public void Drop()
        {
            if (!Alive)
            {
                return;
            }

            OnDrop();
            Alive = false;
            Dropped?.Invoke(this);
            Dropped = null;
        }

        protected virtual void OnAlive()
        {
        }

        protected virtual void OnDrop()
        {
        }
    }
}