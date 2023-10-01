using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; }
        public bool IsAlive { get; protected set; }
        public event Action<IDroppable> Alived;
        public event Action<IDroppable> Dropped;

        protected readonly IDroppable parent;

        protected BaseDroppable(IDroppable parent)
        {
            this.parent = parent;
            Name = GetType().Name;
        }

        public virtual TDroppable GetObject<TDroppable>()
        {
            return this is TDroppable result ? result : default;
        }

        public void SetAlive()
        {
            if (IsAlive)
                return;
            
            OnAlive();
            Alived?.Invoke(this);
        }

        public void Drop()
        {
            if (!IsAlive)
                return;
            
            OnDrop();
            Dropped?.Invoke(this);
            Alived = null;
            Dropped = null;
        }

        protected virtual void OnAlive()
        {
            IsAlive = true;
        }

        protected virtual void OnDrop()
        {
            IsAlive = false;
        }
    }
}