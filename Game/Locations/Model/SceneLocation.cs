using System.Linq;
using Core.Locations.Model;
using Game.Contexts;
using GameData;
using UnityEngine.SceneManagement;

namespace Game.Locations
{
    public class SceneLocation : Location
    {
        private Scene scene;
        
        public SceneLocation(LocationSetting setting, IContext ctx) : base(setting, ctx)
        {
            foreach (var objectsSetting in setting.childSettings)
                droppables.Add(objectsSetting.GetInstance(ctx));
            
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
            var isNewScene = chapter.locationSettings.All(s => s is { } sls && sls.SceneName != scene.name);
            if (isNewScene && scene.isLoaded)
                SceneManager.UnloadSceneAsync(scene.name);
            base.OnDrop();
        }
    }
}