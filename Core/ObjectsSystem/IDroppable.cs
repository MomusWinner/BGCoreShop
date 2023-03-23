using System;

namespace Core.ObjectsSystem
{
    public interface IDroppable
    {
        string Name { get; }
        bool Alive { get; }
        event Action<IDroppable> Dropped;
        void Drop();
        void SetAlive(IDroppable parent = null);
    }
}