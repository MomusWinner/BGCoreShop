using Core.Locations.View;
using Core.ObjectsSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Locations.Model
{
    public class Location : BaseDroppable
    {
        public GameObject Root => locationView.Root;
        public string RootSceneName { get; }
        public string RootObjectResourcesPath { get; }

        private readonly LocationView locationView;
        public Location(LocationSetting settings) : base(settings.SceneName)
        {
            RootSceneName = settings.SceneName;
            RootObjectResourcesPath = settings.RootObjectPath;
            locationView = new LocationView(this);
            GEvent.Attach(GlobalEvents.BothLocationLoaded, Initialize);
        }

        public void Refresh()
        {
            if (!Alive)
            {
                SetAlive();

                var mainScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));
                
                locationView.Refresh();
                
                SceneManager.SetActiveScene(mainScene);
            }
        }
        
        private void Initialize(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.BothLocationLoaded, Initialize);
            
            var mainScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));
            
            locationView.Initialize();
            
            SceneManager.SetActiveScene(mainScene);
        }

        protected override void OnDrop()
        {
            locationView.Drop();
        }
    }
}