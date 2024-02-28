using Core.ObjectsSystem;
using Game.Characters.Control;
using Game.LocationObjects;
using UnityEngine;

namespace Game.Characters.Model
{
    public interface ICharacter : IDroppable, ILocationObject
    {
        GameObject Root { get; }
        IReceiver CommandReceiver { get; }
    }
}