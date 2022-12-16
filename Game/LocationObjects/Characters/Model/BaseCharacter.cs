using Core.Entities.Loopables;
using Core.Locations.Model;
using Game.Characters.Control;
using Game.Characters.View;
using Game.LocationObjects;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacter<TCharacterView, TControl, TBaseCharacterSetting> : LocationObject, ICharacter
        where TCharacterView : BaseCharacterView
        where TControl : ControlLoopable
        where TBaseCharacterSetting : BaseCharacterSetting
    {
        public GameObject Root => View?.Root;
        public virtual IReceiver CommandReceiver => View;
        protected TCharacterView View { get; set; }
        protected TControl Control { get; set; }

        protected readonly TBaseCharacterSetting setting;

        protected BaseCharacter(Location parentLocation, string name, TBaseCharacterSetting setting) : base(parentLocation, name)
        {
            this.setting = setting;
        }
    }
}