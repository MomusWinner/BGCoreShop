using UnityEngine;

namespace Game
{
    public abstract class Component<T> : MonoBehaviour where T : Component
    {
        public T LegacyComponent => legacyComponent;
        [SerializeField] private T legacyComponent;

        protected virtual void Awake()
        {
            legacyComponent ??= GetComponentInChildren<T>();
        }
    }
}