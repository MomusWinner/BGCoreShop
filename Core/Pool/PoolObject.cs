using UnityEngine;

namespace Core.Pool
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
            catch 
            {
                // ignored
            }
        }
    }
}