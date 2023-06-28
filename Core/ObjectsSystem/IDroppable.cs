using System;
using Core.Locations.Model;

namespace Core.ObjectsSystem
{
    public interface IDroppable
    {
        string Name { get; }
        bool Alive { get; }
        event Action<IDroppable> OnLively; 
        event Action<IDroppable> OnDropped;
        void Drop();
        void SetAlive();
    }
}