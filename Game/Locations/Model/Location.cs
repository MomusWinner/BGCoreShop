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
        public GameObject Root => view.Root;

        protected readonly LocationView view;
        protected readonly IContext context;
        protected readonly IList<IDroppable> droppables = new List<IDroppable>();
        protected readonly LocationSetting setting;

        protected Location(LocationSetting setting, IContext context)
        {
            this.context = context;
            this.setting = setting;
            view = new LocationView(setting, context);
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

        protected override void OnAlive()
        {
            if (view is null)
                return;
            view.SetAlive(location);
            SetAliveChildren();
        }
        
        protected virtual void SetAliveChildren()
        {
            foreach (var droppable in droppables)
                droppable?.SetAlive(location);
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