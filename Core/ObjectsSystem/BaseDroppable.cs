using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; protected set; }
        public event Action<IDroppable> OnLively;
        public event Action<IDroppable> OnDropped;

        protected readonly IDroppable parent;

        protected BaseDroppable(IDroppable parent)
        {
            this.parent = parent;
            Name = GetType().Name;
        }

        public void SetAlive()
        {
            if(Alive)
                return;
            
            Alive = true;
            OnAlive();
            OnLively?.Invoke(this);
        }

        public void Drop()
        {
            if (!Alive)
                return;

            OnDrop();
            Alive = false;
            OnDropped?.Invoke(this);
            OnDropped = null;
            OnLively = null;
        }

        protected virtual void OnAlive()
        {
        }

        protected virtual void OnDrop()
        {
        }
    }
}