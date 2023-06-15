using Core.ObjectsSystem;
using Game.Settings.UISettings;
using GameData;
using UnityEditor;
using UnityEngine;

namespace UI.View
{
    public abstract class UiElementView<TSetting, TComponent> : BaseDroppable
        where TComponent : Component, IUIGraphicComponent
        where TSetting : UISetting
    {
        public TComponent Root { get; protected set; }
        protected readonly UiContext context;
        protected readonly TSetting setting;
        private readonly TComponent rootResource;
        
        protected UiElementView(TSetting setting, UiContext ctx)
        {
            this.setting = setting;
            rootResource = Resources.Load<TComponent>(setting.RootObjectPath);
            context = ctx;
        }
        
        public void Show()
        {
            Root.Show();
            OnShow();
        }
        
        public void Hide()
        {
            Root.Hide();
            OnHide();
        }

        public void AddChildComponent(IUIGraphicComponent graphic)
        {
            Root.GraphicMaskable.AddMaskable(graphic.GraphicMaskable);
            OnAddChildComponent(graphic);
        }

        public void RemoveChildComponent(IUIGraphicComponent graphic)
        {
            Root.GraphicMaskable.RemoveMaskable(graphic.GraphicMaskable);
            OnRemoveChildComponent(graphic);
        }

        protected virtual void OnAddChildComponent(IUIGraphicComponent component)
        {
        }

        protected virtual void OnRemoveChildComponent(IUIGraphicComponent component)
        {
            
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            if (rootResource is null)
            {
                Debug.LogWarning($"{Name} for {setting.name} not created");
                return;
            }

            var parent = context is {ParentUiElement: { }} ? context.ParentUiElement.ContentHolder : location?.Root.transform;
            Root = Object.Instantiate(rootResource, parent);
            Root.name = $"[{GetType().Name}] {rootResource.name}";
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root.gameObject);
        }
        
        protected virtual void OnShow()
        {
        }
        
        protected virtual void OnHide()
        {
        }

    }
}