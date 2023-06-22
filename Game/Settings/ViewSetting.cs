using Core;
using Core.ObjectsSystem;
using GameData;
using UnityEngine;

namespace Game.Settings
{
    public abstract class ViewSetting : BaseSetting
    {
#if UNITY_EDITOR
        public Object rootObject;
#endif
        public string rootObjectPath;

        public abstract BaseDroppable GetViewInstance<TContext>(TContext context, IDroppable parent) where TContext : IContext;
        
        public virtual void GetReference()
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