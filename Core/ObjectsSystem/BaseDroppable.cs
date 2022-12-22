using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; protected set; } = true;
        public event Action<IDroppable> Dropped;

        protected BaseDroppable(string name)
        {
            Name = name;
        }
        
        public void SetAlive()
        {
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