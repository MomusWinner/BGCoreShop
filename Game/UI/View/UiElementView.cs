using Core.ObjectsSystem;
using Game.Settings.UISettings;
using GameData;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI.View
{
    public abstract class UiElementView<TSetting, TComponent> : BaseDroppable
        where TComponent : Component, IUIGraphicComponent
        where TSetting : UISetting
    {
        
        public TComponent Root { get; protected set; }
        protected readonly UiContext context;
        protected readonly TSetting setting;
        private TComponent rootResource;
        private bool isResourceLoaded;
        private AsyncOperationHandle<TComponent> opHandler;

        protected UiElementView(TSetting setting, UiContext ctx)
        {
            isResourceLoaded = false;
            this.setting = setting;
            opHandler = Addressables.LoadAssetAsync<TComponent>(this.setting.rootObjectPath);
            opHandler.Completed += handle =>
            {
                rootResource = handle.Result;
                if (!rootResource)
                {
                    Debug.LogWarning($"{GetType()} is not loaded");
                    return;
                }

                Root = Object.Instantiate(rootResource);
                Root.name = $"[{GetType().Name}] {rootResource.name}";
                isResourceLoaded = true;
            };
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

            var parent = context is {ParentUiElement: { }} ? context.ParentUiElement.ContentHolder : null;
            Root.transform.SetParent(parent);
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