using System;
using UnityEngine;

namespace Core.Pool
{
    [Serializable]
    public class PoolNote
    {
        public string Name => prefab != null ? prefab.name : null;
        [HideInInspector] public string name;
        public PoolObject prefab;
        [SerializeField] public string resourcePath;
        public int startCount;
        public ObjectPooling objectPooling;

        public void OnValidate()
        {
            name = prefab.name;
        }
    }
}