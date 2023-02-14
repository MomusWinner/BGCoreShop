using System.Data;
using System.Linq;
using BGCore.Game.Factories;
using Core.Locations.Model;
using GameData;
using GameLogic.GameData.Contexts;
using UnityEngine.SceneManagement;

namespace GameLogic.Locations
{
    public class SceneLocation : Location
    {
        private Scene scene;
        
        public SceneLocation(SceneLocationSetting setting, IContext ctx) : base(setting, ctx)
        {
            foreach (var objectsSetting in setting.childSettings)
                droppables.Add(Factory.CreateItem(objectsSetting, ctx));
            scene = SceneManager.GetSceneByName(setting.SceneName);
            if (scene is {isLoaded: true})
            {
                SetAlive(this);
                return;
            }
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

        protected override void OnDrop()
        {
            var chapter = context.GetContext<MainContext>().CurrentChapter;
            var isNewScene = chapter.locationSettings.All(s => s is SceneLocationSetting sls && sls.SceneName != scene.name);
            if (isNewScene && scene.isLoaded)
                SceneManager.UnloadSceneAsync(scene.name);
            base.OnDrop();
        }
    }
}