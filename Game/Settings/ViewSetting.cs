using Core;
using UnityEngine;

namespace Game.Settings
{
    public abstract class ViewSetting : BaseSetting
    {
        public string RootObjectPath => rootObjectPath;

        [SerializeField] private Object rootObject;
        [SerializeField] private string rootObjectPath;

        protected virtual void OnValidate()
        {
            if (rootObject)
            {
#if UNITY_EDITOR
                rootObjectPath = Utilities.GetValidPathToResource(rootObject);
#endif
            }
        }
    }
}