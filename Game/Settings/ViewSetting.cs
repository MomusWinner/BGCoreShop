using Core;
using UnityEngine;

namespace Game.Settings
{
    public abstract class ViewSetting : BaseSetting
    {
        public string RootObjectPath => rootObjectPath;

        [SerializeField] private Object rootObject;
        [SerializeField] private string rootObjectPath;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (rootObject)
            {
                rootObjectPath = Utilities.GetValidPathToResource(rootObject);
            }
        }
#endif
    }
}