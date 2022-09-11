using UnityEngine;

namespace Game.Characters.View
{
    public interface ICharacterView 
    {
        GameObject Root { get; }
        Transform ParentTransform { get; }
    }
}