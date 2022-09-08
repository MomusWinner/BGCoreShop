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
            rootObject = Object.Instantiate(rootResource);
            rootObject.name = rootResource.name;
        }
    }
}