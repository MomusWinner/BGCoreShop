using Core.ObjectsSystem;
using Game.Characters.View;
using Game.Characters.Control;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacter<TCharacterView, TCharacterControl> : BaseDroppable, ICharacter
        where TCharacterView : BaseCharacterView
        where TCharacterControl : BaseCharacterControl
    {
        public GameObject Root => View?.Root;
        public Transform Pivot => View?.ParentTransform;

        protected abstract TCharacterView View { get; set; }
        protected abstract TCharacterControl Control { get; set; }

        protected BaseCharacterSetting characterSetting;

        protected BaseCharacter(string name, BaseCharacterSetting setting) : base(name)
        {
            characterSetting = setting;
        }
    }
}