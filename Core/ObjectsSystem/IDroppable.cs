using System;

namespace Core.ObjectsSystem
{
    public interface IDroppable
    {
        bool Alive { get; }
        event Action<IDroppable> Dropped;
        void Drop();
        void SetAlive(IDroppable parent = null);
    }
}