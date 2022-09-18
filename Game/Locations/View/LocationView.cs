using Core.Locations.Model;
using Core.ObjectsSystem;
using UnityEngine;

namespace Core.Locations.View
{
    public abstract class LocationView : BaseDroppable
    {
        public GameObject Root { get; private set; }
        
        protected Location ParentLocation { get; }
        private readonly GameObject rootResource;

        protected LocationView(Location location) : base(location.Name)
        {
            ParentLocation = location;
            rootResource = Resources.Load<GameObject>(location.RootObjectResourcesPath);
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
            Root.name = rootResource.name;
        }
        
        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}