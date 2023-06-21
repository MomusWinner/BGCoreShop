using Core.Entities.Loopables;
using Core.ObjectsSystem;
using Game.Characters.Control;
using Game.Characters.View;
using Game.LocationObjects;
using GameData;
using GameLogic.Views;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacter<TCharacterView, TControl, TBaseCharacterSetting> : LocationObject<TCharacterView, TBaseCharacterSetting, Transform>, ICharacter
        where TCharacterView : LocationObjectView<TBaseCharacterSetting, Transform>, IReceiver
        where TControl : ControlLoopable
        where TBaseCharacterSetting : BaseCharacterSetting
    {
        public GameObject Root => view?.Root.gameObject;
        public virtual IReceiver CommandReceiver => view;
        protected TControl Control { get; set; }

        protected readonly TBaseCharacterSetting setting;

        protected BaseCharacter(TBaseCharacterSetting setting, IContext context, IDroppable parent) : base(setting, context, parent)
        {
            this.setting = setting;
        }
    }
}