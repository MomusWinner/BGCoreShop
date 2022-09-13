using Core.ObjectsSystem;
using UnityEngine;

namespace Game.Characters.Model
{
    public interface ICharacter : IDroppable
    {
        GameObject Root { get; }
        Transform ParentTransform { get; }
    }
}