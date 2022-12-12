using Core.Entities;
using Core.Entities.Loopables;
using Core.Locations.Model;
using Game.Characters.View;
using Game.LocationObjects;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacter<TCharacterView, TControl> : LocationObject, ICharacter
        where TCharacterView : BaseCharacterView
        where TControl : ControlLoopable
    {
        public GameObject PlayerRoot => View?.Root;
        public IControllable CharacterController { get; set; }
        public Transform ParentTransform => View?.ParentTransform;
        protected abstract TCharacterView View { get; set; }
        protected abstract TControl Control { get; set; }

        protected BaseCharacterSetting characterSetting;

        protected BaseCharacter(Location parentLocation, string name, BaseCharacterSetting setting) : base(parentLocation, name)
        {
            characterSetting = setting;
        }

        public abstract void Move(ISignal signal);
    }
}