using System;
using UnityEngine;

namespace Core.Main.Pool
{
    public abstract class PoolObject : MonoBehaviour
    {
        private Transform parentObject;

        public void Initiate(Transform parentPoolObject)
        {
            parentObject = parentPoolObject;
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            try
            {
                gameObject.SetActive(false);
                gameObject.transform.SetParent(parentObject);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}