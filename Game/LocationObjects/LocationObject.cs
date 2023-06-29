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
        public int LoadOrder { get; private set; }
        
        public void SetLoadOrder(int order)
        {
            LoadOrder = order;
            view.SetLoadOrder(order);
        }

        public virtual Transform Transform => view.Transform;
        
        protected TView view;
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
            var isOrdered = parent is not Location location || location.CurrentAliveChild >= LoadOrder;
            return parent is {Alive: true} && view is {Alive: true} && isOrdered;
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