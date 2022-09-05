using System;

namespace Core.Main.ObjectsSystem
{
    public interface IDroppable
    {
        string Name { get; }
        bool Alive { get; }
        event Action<IDroppable> Dropped;
        void Drop();
        void SetAlive();
    }
}