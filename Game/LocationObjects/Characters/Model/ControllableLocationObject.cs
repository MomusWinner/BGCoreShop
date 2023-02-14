using Core;
using Core.Entities.Loopables;
using Game.Characters.Control;
using Game.Characters.View;
using Game.LocationObjects;
using Game.Settings;
using GameData;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class ControllableLocationObject<TView, TControl, TSetting, TObject> : BaseLocationObject<TView, TSetting>, ICharacter
        where TView : ReceiverView<TSetting, TObject>
        where TControl : ControlLoopable
        where TSetting : BaseLocationObjectSetting
        where TObject : Component
    {
        public GameObject Root => view?.Root.gameObject;
        public virtual IReceiver CommandReceiver => view;
        protected TControl Control { get; }

        protected ControllableLocationObject(TSetting setting, IContext context) : base(setting, context)
        {
            Control = Utilities.Instantiate<TControl>(setting, context);
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            Control.SetAlive();
        }

        protected override void OnDrop()
        {
            Control.Drop();
            base.OnDrop();
        }
    }
}