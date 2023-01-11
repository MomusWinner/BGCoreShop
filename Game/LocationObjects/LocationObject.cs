using System;
using Core.ObjectsSystem;
using GameData;
using GameLogic.GameData.Contexts;
using UnityEngine;

namespace Game.LocationObjects
{
    public abstract class LocationObject<TView> : BaseDroppable, ILocationObject where TView : BaseDroppable
    {
        public Guid Id { get; } = Guid.NewGuid();
        protected TView view;
        protected IContext context;
        
        protected LocationObject(IContext context)
        {
            this.context = context.GetContext<GeneralContext>();
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            view?.SetAlive();
            context.GetContext<LocationObjectInventory>().AddObject(this);
        }

        protected override void OnDrop()
        {
            context.GetContext<LocationObjectInventory>().RemoveObject(Id);
            base.OnDrop();
            view?.Drop();
            view = null;
        }
    }

    public interface ILocationObject
    {
        public Guid Id { get; }
    }
}