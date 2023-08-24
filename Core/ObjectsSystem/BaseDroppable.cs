using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public virtual bool IsAlive { get; private set; }
        public event Action<IDroppable> Dropped;

        protected readonly IDroppable parent;

        protected BaseDroppable(IDroppable parent)
        {
            this.parent = parent;
            Name = GetType().Name;
        }
        
        public void SetAlive()
        {
            IsAlive = true;
            OnAlive();
        }
        
        public void Drop()
        {
            if (!IsAlive)
            {
                return;
            }

            OnDrop();
            IsAlive = false;
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