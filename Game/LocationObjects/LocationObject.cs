using System;
using Core;
using Core.ObjectsSystem;
using Game.Contexts;
using GameData;
using UnityEngine;

namespace Game.LocationObjects
{
    public abstract class LocationObject<TView> : BaseDroppable, ILocationObject where TView : BaseDroppable, ILocationObject
    {
        public Guid Id { get; }
        public Transform Transform => view.Transform;
        protected TView view;
        protected readonly IContext context;

        protected LocationObject(IContext context)
        {
            Id = Guid.NewGuid();
            this.context = context.GetContext<MainContext>();
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            view?.SetAlive(parent);
            context.GetContext<LocationContext>().AddObject(this);
        }

        protected override void OnDrop()
        {
            context.GetContext<LocationContext>().RemoveObject(Id);
            base.OnDrop();
            view?.Drop();
        }
    }
}