using System;
using Core.ObjectsSystem;
using GameData;
using GameLogic.GameData.Contexts;

namespace Game.LocationObjects
{
    public abstract class LocationObject<TView> : BaseDroppable, ILocationObject where TView : BaseDroppable
    {
        public Guid Id { get; } 
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

    public interface ILocationObject
    {
        public Guid Id { get; }
    }
}