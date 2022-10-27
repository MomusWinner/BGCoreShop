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

        protected LocationView(Location location, IContext ctx) : base(location.Name)
        {
            ParentLocation = location;
            rootResource = Resources.Load<GameObject>(location.RootObjectResourcesPath);
            context = ctx;
        }

        public void Initialize()
        {
            if (rootResource)
            {
                InnerInitialize();
            }
        }
        
        public void Refresh()
        {
            if (!Alive)
            {
                Initialize();
                SetAlive();
            }
        }
        
        protected virtual void InnerInitialize()
        {
            Root = Object.Instantiate(rootResource);
            Root.name = "[Location Root] " + rootResource.name;
        }
        
        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}