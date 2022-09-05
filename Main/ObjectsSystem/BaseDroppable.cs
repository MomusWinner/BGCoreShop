using System;

namespace Core.Main.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; }
        public bool Alive { get; private set; } = true;
        public event Action<IDroppable> Dropped;

        protected BaseDroppable(string name)
        {
            Name = name;
        }
        
        protected virtual void OnDrop()
        {
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

        public virtual void SetAlive()
        {
            Alive = true;
        }
    }
}