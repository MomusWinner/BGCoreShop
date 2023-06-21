using System;
using UnityEngine;

namespace UI
{
    public abstract class Component<T> : MonoBehaviour where T : Component
    {
        public T LegacyComponent => legacyComponent;
        [SerializeField] private T legacyComponent;

        protected virtual void OnEnable()
        {
            legacyComponent ??= GetComponentInChildren<T>();
        }

        protected virtual void OnDisable()
        {
        }
    }
}