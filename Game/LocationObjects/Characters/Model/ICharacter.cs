using Core.ObjectsSystem;
using UnityEngine;

namespace Game.Characters.Model
{
    public interface ICharacter : IDroppable
    {
        GameObject ViewRoot { get; }
        Transform ParentTransform { get; }
    }
}