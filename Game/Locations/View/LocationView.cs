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
            }
        }

        public void Refresh()
        {
            if (!Alive)
            {
                SetAlive();
                Initialize();
            }
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}