using Core;
using UnityEngine;

namespace Game.Settings
{
    public abstract class ViewSetting : BaseSetting
    {
        public Object rootObject;
        public string rootObjectPath;

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