using System;
using Core;
using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.Contexts;
using Game.Settings;
using GameData;
using GameLogic.Views;
using UnityEngine;

namespace Game.LocationObjects
{
    public abstract class LocationObject<TView, TSetting, TObject> : BaseDroppable, ILocationObject
        where TView : LocationObjectView<TSetting, TObject>
        where TSetting : ViewSetting
        where TObject : Component
    {
        public Guid Id { get; }
        
        public virtual Transform Transform => view.Transform;
        
        protected readonly TView view;
        protected readonly IContext context;

        protected LocationObject(TSetting setting, IContext context, IDroppable parent) : base(parent)
        {
            Id = Guid.NewGuid();
            this.context = context?.GetContext<MainContext>();
            view = (TView) setting.GetViewInstance(context, parent);
            Scheduler.InvokeWhen(IsReadyForAliVe, SetAlive);
        }

        protected virtual bool IsReadyForAliVe()
        {
            return parent is {Alive: true} && view is {Alive: true};
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            view.SetAlive();
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            view?.Drop();
        }
    }
}