using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; }
        public bool IsAlive { get; protected set; }
        public event Action<IDroppable> Dropped;

        protected readonly IDroppable parent;

        protected BaseDroppable(IDroppable parent)
        {
            this.parent = parent;
            Name = GetType().Name;
        }
        
        public void SetAlive()
        {
            OnAlive();
        }
        
        public void Drop()
        {
            if (!IsAlive)
            {
                return;
            }

            OnDrop();
            Dropped?.Invoke(this);
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