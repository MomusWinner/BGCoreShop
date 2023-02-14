using System;
using Core;
using Core.ObjectsSystem;
using Game.Settings;
using GameData;
using GameLogic.GameData.Contexts;

namespace Game.LocationObjects
{
    public abstract class BaseLocationObject<TView, TSetting> : BaseDroppable, ILocationObject
        where TView : BaseDroppable
        where TSetting : BaseLocationObjectSetting
    {
        public Guid Id { get; }
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
            view?.SetAlive(location);
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