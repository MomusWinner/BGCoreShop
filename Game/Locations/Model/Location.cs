using System.Threading;
using Core.Locations.View;
using Core.ObjectsSystem;
using Game.Locations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Locations.Model
{
    public abstract class Location : BaseDroppable
    {
        public GameObject Root => locationView.Root;
        public string RootSceneName { get; }
        public string RootObjectResourcesPath { get; }

        protected readonly LocationView locationView;

        protected Location(LocationSetting settings) : base(settings.SceneName)
        {
            RootSceneName = settings.SceneName;
            RootObjectResourcesPath = settings.RootObjectPath;

            locationView = LocationViewFactory.CreateView(this);

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

                SceneManager.SetActiveScene(mainScene);
            }
        }
        
        protected virtual void InitializeView(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.LocationScenesLoaded, InitializeView);
            GEvent.Attach(GlobalEvents.LocationViewLoaded, InitializeChildViews);

            if (locationView is { })
            {
                var mainScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));

                locationView.Initialize();

                SceneManager.SetActiveScene(mainScene);
            }
        }

        private void InitializeChildViews(params object[] objects)
        {
            if (objects[0] == locationView)
            {
                GEvent.Detach(GlobalEvents.LocationViewLoaded, InitializeChildViews);
                InitializeChildViews();
            }
        }

        protected abstract void InitializeChildViews();

        protected override void OnDrop()
        {
            locationView?.Drop();
        }
    }
}