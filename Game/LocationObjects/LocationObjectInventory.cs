using System;
using System.Collections.Generic;
using GameData;

namespace Game.LocationObjects
{
    public class LocationObjectInventory : BaseContext
    {
        private readonly Dictionary<Guid, ILocationObject> objectContexts = new Dictionary<Guid, ILocationObject>();

        public TLocationObject Get<TLocationObject>(Guid passengerId) where TLocationObject : ILocationObject
        {
            return objectContexts.TryGetValue(passengerId, out var context) ? (TLocationObject) context : default;
        }

        public void AddObject(ILocationObject locationObject)
        {
            objectContexts.Add(locationObject.Id, locationObject);
        }

        public void RemoveObject(Guid id)
        {
            objectContexts.Remove(id);
        }
    }
}