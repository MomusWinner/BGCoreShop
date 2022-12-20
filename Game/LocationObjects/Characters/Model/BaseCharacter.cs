using Core.Entities.Loopables;
using Core.Locations.Model;
using Game.Characters.Control;
using Game.Characters.View;
using Game.LocationObjects;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacter<TCharacterView, TControl, TBaseCharacterSetting> : LocationObject<TCharacterView>, ICharacter
        where TCharacterView : BaseCharacterView
        where TControl : ControlLoopable
        where TBaseCharacterSetting : BaseCharacterSetting
    {
        public GameObject Root => view?.Root;
        public virtual IReceiver CommandReceiver => view;
        protected TControl Control { get; set; }

        protected readonly TBaseCharacterSetting setting;

        protected BaseCharacter(string name, TBaseCharacterSetting setting) : base(name)
        {
            this.setting = setting;
        }
    }
}