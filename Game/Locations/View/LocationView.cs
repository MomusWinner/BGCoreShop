using Core.Locations.Model;
using Core.ObjectsSystem;
using UnityEngine;

namespace Core.Locations.View
{
    public abstract class LocationView : BaseDroppable
    {
        public GameObject Root { get; private set; }
        private readonly GameObject rootResource;

        protected LocationView(Location location) : base(location.Name)
        {
            rootResource = Resources.Load<GameObject>(location.RootObjectResourcesPath);
        }

        public virtual void Initialize()
        {
            if (rootResource)
            {
                Root = Object.Instantiate(rootResource);
                Root.name = rootResource.name;
                GEvent.Call(GlobalEvents.LocationViewLoaded, this);
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

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}