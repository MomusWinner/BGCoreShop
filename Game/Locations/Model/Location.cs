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

        protected readonly LocationView locationView;
        protected IContext context;
        
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

                InitializeChildViews();
                
                SceneManager.SetActiveScene(mainScene);
            }
        }

        protected virtual void InitializeView(params object[] objects)
        {
            Debug.Log($"Start initialize view {Name}");
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
            Debug.Log($"Stop initialize view {Name}");
        }
        
        protected abstract void InitializeChildViews();

        protected override void OnDrop()
        {
            locationView?.Drop();
        }
    }
}