using Core;
using Core.ObjectsSystem;
using GameData;
using UnityEngine;

namespace Game.Settings
{
    public abstract class ViewSetting : BaseSetting
    {
        public Object rootObject;
        public string rootObjectPath;

        public abstract BaseDroppable GetViewInstance<TContext>(TContext context, IDroppable parent) where TContext : IContext;
        
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