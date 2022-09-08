using Core.Main.ObjectsSystem;
using UnityEngine;

namespace Core.Main.Locations
{
    public class LocationView : BaseDroppable
    {
        private GameObject rootObject;
        private GameObject rootResource;

        public LocationView(Location location) : base(location.Name)
        {
            rootResource = location.RootObjectResources;
        }

        public void Initialize()
        {
            if (rootResource)
            {
                rootObject = Object.Instantiate(rootResource);
                rootObject.name = rootResource.name;
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
            Object.Destroy(rootObject);
        }
    }
}