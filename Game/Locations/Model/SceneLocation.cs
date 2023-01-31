using BGCore.Game.Factories;
using Core.Locations.Model;
using GameData;
using UnityEngine.SceneManagement;

namespace GameLogic.Locations
{
    public class SceneLocation : Location
    {
        private Scene scene;
        
        public SceneLocation(SceneLocationSetting setting, IContext ctx) : base(setting, ctx)
        {
            foreach (var objectsSetting in setting.locationObjectsSettings)
                droppables.Add(Factory.CreateItem(objectsSetting, ctx));
            var operation = SceneManager.LoadSceneAsync(setting.SceneName, LoadSceneMode.Additive);
            operation.completed += _ =>
            {
                scene = SceneManager.GetSceneByName(setting.SceneName);
                SetAlive(this);
            };
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            SceneManager.MoveGameObjectToScene(Root, scene);
        }
    }
}