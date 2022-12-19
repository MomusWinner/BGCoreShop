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

        private readonly LocationView locationView;
        protected readonly IContext context;
        protected readonly IList<IDroppable> droppables = new List<IDroppable>();

        protected Location(LocationSetting settings, IContext baseContext) : base(settings.SceneName)
        {
            RootSceneName = settings.SceneName;
            RootObjectResourcesPath = settings.RootObjectPath;
            context = baseContext;

            locationView = GeneralFactory.CreateItem<LocationView, Location>(this, baseContext);

            GEvent.Attach(GlobalEvents.LocationScenesLoaded, InitializeView);
        }

        public void Refresh()
        {
            if (!Alive && locationView is { })
            {
                SetAlive();

                var mainScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));

                locationView.Refresh();
                GEvent.Call(GlobalEvents.LocationViewLoaded, this);

                SetAliveLocationObjects();

                SceneManager.SetActiveScene(mainScene);
            }
        }
        
        public TDroppable GetFirstOrDefaultObject<TDroppable>(Func<TDroppable, bool> predicate)
            where TDroppable : IDroppable
        {
            return droppables.Where(d => d is TDroppable).Cast<TDroppable>().FirstOrDefault(predicate);
        }

        protected virtual void InitializeView(params object[] objects)
        {
            AddContext();
            GEvent.Call(GlobalEvents.LocationViewLoaded, locationView);
            GEvent.Detach(GlobalEvents.LocationScenesLoaded, InitializeView);

            if (locationView is { })
            {
                var mainScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));

                locationView.Initialize();
                GEvent.Call(GlobalEvents.LocationViewLoaded, this);

                SetAliveLocationObjects();

                SceneManager.SetActiveScene(mainScene);
            }
        }
        
        protected virtual void AddContext()
        {
        }

        protected virtual void RemoveContext()
        {
        }

        protected virtual void SetAliveLocationObjects()
        {
            foreach (var droppable in droppables)
                droppable?.SetAlive();
            RemoveContext();
        }

        protected override void OnDrop()
        {
            locationView?.Drop();
            foreach (var droppable in droppables)
                droppable?.Drop();
            droppables.Clear();
        }
    }
}