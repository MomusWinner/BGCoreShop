using BGCore.Game.Factories;
using Core.Locations.Model;
using GameData;
using GameLogic.GameData;
using GameLogic.GameData.Contexts;
using GameLogic.Gameplay;
using UnityEngine.SceneManagement;

namespace GameLogic.Locations
{
    public class SceneLocation : Location
    {
        private Scene scene;
        
        public SceneLocation(SceneLocationSetting setting, IContext ctx) : base(setting, ctx)
        {
            foreach (var objectsSetting in setting.locationObjectsSettings)
                childrenDroppables.Add(Factory.CreateItem(objectsSetting, ctx));
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

        protected override void AddContext()
        {
            context.AddContext(new SpawnContext());
            context.AddContext(new SprintPointsSetting(16));
        }

        protected override void RemoveContext()
        {
            context.RemoveContext<SpawnContext>();
            context.RemoveContext<GearboxModel>();
            context.RemoveContext<SprintPointsSetting>();
        }
    }
}