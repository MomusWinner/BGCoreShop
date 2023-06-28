using System;
using System.Collections.Generic;
using System.Linq;
using Core.Locations.View;
using Core.ObjectsSystem;
using GameData;
using UnityEngine;

namespace Core.Locations.Model
{
    public abstract class Location : BaseDroppable
    {
        public int CurrentAliveChild { get; protected set; }
        public Transform RootTransform => view.Root.transform;
        protected LocationView view;
        protected readonly IContext context;
        protected readonly IList<IDroppable> droppables = new List<IDroppable>();
        protected readonly LocationSetting setting;

        protected Location(LocationSetting setting, IContext context, IDroppable parent) : base(parent)
        {
            this.context = context;
            this.setting = setting;
            Scheduler.InvokeWhen(() => view is {Alive: true}, SetAlive);
        }

        public IEnumerable<TDroppable> GetAllObjects<TDroppable>()
        {
            return droppables.Where(o => o.GetType() == typeof(TDroppable)).Cast<TDroppable>();
        }

        public TDroppable GetFirstOrDefaultObject<TDroppable>(Func<TDroppable, bool> predicate = null)
            where TDroppable : IDroppable
        {
            return droppables.Where(d => d is TDroppable).Cast<TDroppable>()
                .FirstOrDefault(d => predicate is null || predicate(d));
        }
        
        protected override void OnDrop()
        {
            view?.Drop();
            DropChildren();
        }

        private void DropChildren()
        {
            foreach (var droppable in droppables)
                droppable?.Drop();
            droppables.Clear();
        }
    }
}