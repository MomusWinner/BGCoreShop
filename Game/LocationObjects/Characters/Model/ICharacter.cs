using Core.Entities;
using Core.ObjectsSystem;
using UnityEngine;

namespace Game.Characters.Model
{
    public interface ICharacter : IDroppable
    {
        GameObject PlayerRoot { get; }
        Transform ParentTransform { get; }
        IControllable CharacterController { get; set; }
        void Move(ISignal signal);
    }

    public interface ISignal
    {
    }

    public abstract class AnimatorSignal<TKey, TValue> : ISignal
    {
        public TKey Key { get; }

        public TValue Value { get; }

        protected AnimatorSignal(TKey variableName, TValue value)
        {
            Value = value;
            Key = variableName;
        } 
    }
}