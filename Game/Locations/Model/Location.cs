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
                GEvent.Call(GlobalEvents.LocationViewLoaded, this);

                InitializeChildViews();
                
                SceneManager.SetActiveScene(mainScene);
            }
        }

        protected virtual void InitializeView(params object[] objects)
        {
            GEvent.Call(GlobalEvents.LocationViewLoaded, locationView);
            GEvent.Detach(GlobalEvents.LocationScenesLoaded, InitializeView);

            if (locationView is { })
            {
                var mainScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));

                locationView.Initialize();
                GEvent.Call(GlobalEvents.LocationViewLoaded, this);
                
                InitializeChildViews();

                SceneManager.SetActiveScene(mainScene);
            }
        }
        
        protected abstract void InitializeChildViews();

        protected override void OnDrop()
        {
            locationView?.Drop();
        }
    }
}