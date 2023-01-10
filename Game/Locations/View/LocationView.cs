using Core.Locations.Model;
using Core.ObjectsSystem;
using GameData;
using UnityEngine;

namespace Core.Locations.View
{
    public abstract class LocationView : BaseDroppable
    {
        public GameObject Root { get; private set; }
        
        protected Location ParentLocation { get; }

        protected readonly IContext context;
        private readonly GameObject rootResource;

        protected LocationView(Location location, IContext ctx)
        {
            ParentLocation = location;
            rootResource = Resources.Load<GameObject>(location.RootObjectResourcesPath);
            context = ctx;
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            if(!rootResource)
                return;
            Root = Object.Instantiate(rootResource);
            Root.name = "[Location Root] " + rootResource.name;
        }
        
        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
            Root = null;
        }
    }
}