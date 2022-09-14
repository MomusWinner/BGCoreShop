using Core.Locations.Model;
using Game.Characters.View;
using Game.Characters.Control;
using Game.LocationObjects;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacter<TCharacterView, TCharacterControl> : LocationObject, ICharacter
        where TCharacterView : BaseCharacterView
        where TCharacterControl : BaseCharacterControl
    {
        public GameObject ViewRoot => View?.Root;
        public Transform ParentTransform => View?.ParentTransform;
        protected abstract TCharacterView View { get; set; }
        protected abstract TCharacterControl Control { get; set; }

        protected BaseCharacterSetting characterSetting;

        protected BaseCharacter(Location parentLocation, string name, BaseCharacterSetting setting) : base(parentLocation, name)
        {
            characterSetting = setting;
        }
    }
}