using System;
using System.Collections.Generic;
using System.Linq;
using BGCore.Game.Factories;
using Core.Locations.View;
using Core.ObjectsSystem;
using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Locations.Model
{
    public abstract class Location : BaseDroppable

    {
        public GameObject Root => locationView.Root;
        public string RootSceneName { get; }
        public string RootObjectResourcesPath { get; }

        protected LocationView locationView;
        protected readonly IContext context;
        protected readonly IList<IDroppable> droppables = new List<IDroppable>();
        protected readonly LocationSetting setting;

        protected Location(LocationSetting setting, IContext context)
        {
            Name = setting.SceneName;
            this.setting = setting;
            RootSceneName = setting.SceneName;
            RootObjectResourcesPath = setting.RootObjectPath;
            this.context = context;
            Initialize();
        }

        public void Refresh()
        {
            if (Alive || locationView is null)
                return;

            Initialize();
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
            GEvent.Call(GlobalEvents.LocationViewLoaded, locationView);

            if (locationView is null)
                return;
            AddContext();

            var mainScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));

            locationView.SetAlive(location);

            GEvent.Call(GlobalEvents.LocationViewLoaded, this);

            SetAliveLocationObjects();

            SceneManager.SetActiveScene(mainScene);
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

        protected virtual void SetAliveLocationObjects()
        {
            foreach (var droppable in droppables)
                droppable?.SetAlive(this);
        }

        protected override void OnDrop()
        {
            locationView?.Drop();
            foreach (var droppable in droppables)
                droppable?.Drop();
            droppables.Clear();
            RemoveContext();
        }

        private void Initialize()
        {
            OnInitialize();
        }
    }
}