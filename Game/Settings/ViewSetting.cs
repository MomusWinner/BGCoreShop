using System;
using Core;
using Core.ObjectsSystem;
using GameData;
using Object = UnityEngine.Object;

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
#if UNITY_EDITOR
            if (rootObject)
            {
                rootObjectPath = Utilities.GetValidPathToResource(rootObject);
            }
#endif
        }

        protected void OnValidate()
        {
            GetReference();
        }
    }
}