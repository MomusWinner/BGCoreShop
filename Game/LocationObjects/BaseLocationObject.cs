using System;
using Contexts;
using Core;
using Core.ObjectsSystem;
using Game.Settings;
using GameLogic.GameData.Contexts;
using UnityEngine;

namespace Game.LocationObjects
{
    public abstract class BaseLocationObject<TView, TSetting> : BaseDroppable, ILocationObject
        where TView : BaseDroppable, ILocationObject
        where TSetting : BaseLocationObjectSetting
    {
        public Guid Id { get; protected set; }
        public Transform Transform => view.Transform;
        protected readonly TView view;
        protected readonly IContext context;

        protected BaseLocationObject(TSetting setting, IContext context)
        {
            Id = Guid.NewGuid();
            this.context = context.GetContext<MainContext>();
            view = Utilities.Instantiate<TView>(setting, context);
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            view?.SetAlive(parent);
            context?.GetContext<LocationContext>()?.AddObject(this);
        }

        protected override void OnDrop()
        {
            context?.GetContext<LocationContext>()?.RemoveObject(Id);
            base.OnDrop();
            view?.Drop();
        }
    }
}