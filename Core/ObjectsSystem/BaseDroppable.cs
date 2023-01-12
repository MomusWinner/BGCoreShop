using System;
using Core.Locations.Model;
using Core.Timers;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; protected set; } = true;
        public event Action<IDroppable> Dropped;

        protected Location location;

        protected BaseDroppable()
        {
            Name = GetType().Name;
        }
        
        public void SetAlive(Location location = null)
        {
            Alive = true;
            this.location = location;
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