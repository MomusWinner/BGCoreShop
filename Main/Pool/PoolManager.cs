using System.Collections.Generic;
using System.Linq;
using Storage.GameData.DataContainers;
using UnityEngine;

namespace Core.Main.Pool
{
    public static class PoolManager
    {
        private static Dictionary<string, PoolNote> PoolNotes
        {
            get
            {
                if (staticPoolNotes is null)
                {
                    Initialize(DataBase.Instance.PoolNotes);
                }

                return staticPoolNotes;
            }
        }


        private static GameObject staticObjectsParent;
        private static Dictionary<string, PoolNote> staticPoolNotes;

        private static void Initialize(PoolNote[] poolNotes)
        {
            staticPoolNotes = poolNotes.ToDictionary(n => n.Name, n => n);

            staticObjectsParent = new GameObject("[Pool]");

            foreach (var poolNote in PoolNotes.Values)
            {
                if (string.IsNullOrEmpty(poolNote.resourcePath))
                {
                    continue;
                }

                var sample = Resources.Load<PoolObject>(poolNote.resourcePath);
                poolNote.objectPooling = new ObjectPooling(poolNote.startCount, sample, staticObjectsParent.transform);
            }
        }

        public static T GetObject<T>(string name, Vector3 position, Quaternion rotation, Transform parent)
            where T : PoolObject
        {
            T result = null;

            if (PoolNotes.TryGetValue(name, out var poolNote))
            {
                result = (T) poolNote.objectPooling.GetObject();
                Transform resultTransform;
                (resultTransform = result.transform).SetParent(parent);
                resultTransform.position = position;
                resultTransform.rotation = rotation;
                result.gameObject.SetActive(true);
            }

            return result;
        }

#if UNITY_EDITOR

        public static void OnValidate(PoolNote[] poolNotes)
        {
            if (poolNotes is null)
            {
                return;
            }

            foreach (var poolNote in poolNotes)
            {
                if (poolNote.prefab is null)
                {
                    continue;
                }

                poolNote.resourcePath = Utilities.GetValidPathToResource(poolNote.prefab);

                poolNote.OnValidate();
            }
        }
#endif
    }
}