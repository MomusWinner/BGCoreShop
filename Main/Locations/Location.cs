using Core.Main.ObjectsSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Main.Locations
{
    public class Location : BaseDroppable
    {
        public string RootSceneName { get; private set; }
        public GameObject RootObjectResources { get; }

        private readonly LocationView locationView;
        private readonly string rootObjectPath;
        public Location(LocationSetting settings) : base(settings.SceneName)
        {
            RootSceneName = settings.SceneName;
            rootObjectPath = settings.RootObjectPath;
            RootObjectResources = Resources.Load<GameObject>(rootObjectPath);
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