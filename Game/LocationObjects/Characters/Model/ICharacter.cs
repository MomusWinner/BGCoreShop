using Core.ObjectsSystem;
using Game.Characters.Control;
using UnityEngine;

namespace Game.Characters.Model
{
    public interface ICharacter : IDroppable
    {
        GameObject PlayerRoot { get; }
        IReceiver CommandReceiver { get; }
    }
}