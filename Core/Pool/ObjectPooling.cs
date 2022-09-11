using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Pool
{
    public class ObjectPooling
    {
        private readonly Transform objectsParent;

        private readonly List<PoolObject> objects;

        public ObjectPooling(int startCount, PoolObject sample, Transform parent)
        {
            objects = new List<PoolObject>();
            objectsParent = parent;

            for (var i = 0; i < startCount; i++)
            {
                AddObject(sample, parent);
            }
        }

        public PoolObject GetObject()
        {
            var result = objects.FirstOrDefault(o => !o.gameObject.activeInHierarchy);

            if (result is { })
            {
                return result;
            }

            AddObject(objects[0], objectsParent);

            result = objects.Last();

            return result;
        }

        private void AddObject(PoolObject sample, Transform objectParent)
        {
            var temp = Object.Instantiate(sample, objectParent, true);
            temp.name = sample.name;
            temp.Initiate(objectParent);
            objects.Add(temp);
        }
    }
}