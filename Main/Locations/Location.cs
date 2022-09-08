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

        public Location(LocationSetting settings) : base(settings.SceneName)
        {
            RootSceneName = settings.SceneName;
            RootObjectResources = Resources.Load<GameObject>(settings.RootObjectPath);
            locationView = new LocationView(this);
            GEvent.Attach(GlobalEvents.LocationLoaded, Initialize);
        }

        private void Initialize(params object[] objects)
        {
            GEvent.Detach(GlobalEvents.LocationUnloaded, Initialize);
            
            var mainScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(RootSceneName));
            
            locationView.Initialize();
            
            SceneManager.SetActiveScene(mainScene);

        }

        protected override void OnDrop()
        {
        }
    }
}