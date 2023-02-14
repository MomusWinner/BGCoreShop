using System;
using System.Collections.Generic;
using System.Linq;
using GameData;

namespace Game.LocationObjects
{
    public class LocationContext : BaseContext
    {
        private readonly Dictionary<Guid, ILocationObject> objectContexts = new Dictionary<Guid, ILocationObject>();

        public TLocationObject Get<TLocationObject>(Guid objectId = default) where TLocationObject : ILocationObject
        {
            if (objectId == default)
                return Gets<TLocationObject>().FirstOrDefault();
            return objectContexts.TryGetValue(objectId, out var context) ? (TLocationObject) context : default;
        }

        private TLocationObject[] Gets<TLocationObject>()
        {
            return objectContexts.Values.Where(o => o is TLocationObject).Cast<TLocationObject>().ToArray();
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