using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.Locations;
using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Locations.View
{
    public class LocationView : BaseDroppable
    {
        public GameObject Root { get; private set; }

        protected readonly IContext context;
        private readonly GameObject resources;

        public LocationView(LocationSetting setting, IContext ctx, IDroppable parent) : base(parent)
        {
            resources = Resources.Load<GameObject>(setting.rootObjectPath);
            context = ctx;
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            if (!resources)
                return;
            Root = Object.Instantiate(resources);
            Root.name = $"[{GetType().Name}]" + resources.name;
            SceneManager.MoveGameObjectToScene(Root, ((SceneLocation) parent).Scene);
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
            Root = null;
        }
    }
}