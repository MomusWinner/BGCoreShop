using Core.ObjectsSystem;
using Game.UI;
using GameData;
using UnityEngine;

namespace Game.Settings.UISettings
{
    public abstract class UISetting : ViewSetting
    {
        public bool showOnAlive = true;
        public UISetting[] childUiElementSettings;

        public override IDroppable GetInstance<TContext>(TContext context)
        {
            if (context is UiContext uiContext)
                return GetInstance(uiContext);
            Debug.LogWarning($"{GetType()} not create instance");
            return null;
        }

        public override BaseDroppable GetViewInstance<TContext>(TContext context)
        {
            if (context is UiContext uiContext)
                return GetViewInstance(uiContext);
            Debug.LogWarning($"{GetType()} not create view instance");
            return null;
        }

        protected abstract IUiElement GetInstance(UiContext context);
        protected abstract BaseDroppable GetViewInstance(UiContext context);
    }
}