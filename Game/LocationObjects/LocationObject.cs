using System;
using Core;
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
        public Transform Transform => view.Transform;
        protected TView view;
        protected readonly IContext context;

        protected LocationObject(TSetting setting, IContext context, IDroppable parent) : base(parent)
        {
            Id = Guid.NewGuid();
            this.context = context?.GetContext<MainContext>();
            view = (TView) setting.GetViewInstance(context, parent);
            Scheduler.InvokeWhen(() => parent is {Alive: true} && view is {Alive: true}, SetAlive);
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            context?.GetContext<LocationContext>().AddObject(this);
        }

        protected override void OnDrop()
        {
            context.GetContext<LocationContext>().RemoveObject(Id);
            base.OnDrop();
            view?.Drop();
        }
    }
}