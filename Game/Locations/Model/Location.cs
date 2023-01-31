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
    protected readonly IList<IDroppable> childrenDroppables = new List<IDroppable>();
    protected readonly LocationSetting setting;

    protected Location(LocationSetting setting, IContext context)
    {
        this.context = context;
        this.setting = setting;
        view = new LocationView(setting, context);
        Initialize();
    }

    public IEnumerable<TDroppable> GetAllObjects<TDroppable>()
    {
        return childrenDroppables.Where(o => o.GetType() == typeof(TDroppable)).Cast<TDroppable>();
    }

    public TDroppable GetFirstOrDefaultObject<TDroppable>(Func<TDroppable, bool> predicate = null)
        where TDroppable : IDroppable
    {
        return childrenDroppables.Where(d => d is TDroppable).Cast<TDroppable>()
            .FirstOrDefault(d => predicate is null || predicate(d));
    }

    protected override void OnAlive()
    {
        if (view is null)
            return;
        AddContext();
        view.SetAlive(location);
        SetAliveChildren();
    }

    protected virtual void AddContext()
    {
    }

    protected virtual void RemoveContext()
    {
    }

    protected virtual void OnInitialize()
    {
    }

    protected virtual void SetAliveChildren()
    {
        foreach (var droppable in childrenDroppables)
            droppable?.SetAlive(location);
    }

    protected override void OnDrop()
    {
        view?.Drop();
        DropChildren();
        RemoveContext();
    }

    private void DropChildren()
    {
        foreach (var droppable in childrenDroppables)
            droppable?.Drop();
        childrenDroppables.Clear();
    }

    private void Initialize()
    {
        OnInitialize();
    }
    }
}