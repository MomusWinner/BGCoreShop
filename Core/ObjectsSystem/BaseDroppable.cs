using System;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; protected set; }
         = true;
        public event Action<IDroppable> Dropped;

        protected BaseDroppable(string name)
        {
            Name = name;
        }
        
        protected virtual void OnDrop()
        {
        }
        
        public virtual void SetAlive()
        {
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
    }
}