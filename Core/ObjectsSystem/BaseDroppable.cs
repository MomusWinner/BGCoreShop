using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; private set; }
        public event Action<IDroppable> Dropped;

        protected IDroppable parent;

        protected BaseDroppable(IDroppable parent)
        {
            this.parent = parent;
            Name = GetType().Name;
        }
        
        public void SetAlive(IDroppable parent = null)
        {
            this.parent = parent;
            OnAlive();
            Alive = true;
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