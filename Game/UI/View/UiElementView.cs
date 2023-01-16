using System.Collections.Generic;
using Core.ObjectsSystem;
using Game.Settings.UISettings;
using GameData;
using UnityEngine;

namespace UI.View
{
    public abstract class UiElementView<TSetting, TComponent> : BaseDroppable
        where TComponent : Component
        where TSetting : UISetting
    {
        public TComponent Root { get; protected set; }
        protected readonly UiContext context;
        protected readonly TSetting setting;
        private readonly TComponent rootResource;

        private readonly List<Component> childComponents = new List<Component>();

        protected UiElementView(TSetting setting, UiContext ctx)
        {
            this.setting = setting;
            rootResource = Resources.Load<TComponent>(setting.RootObjectPath);
            context = ctx;
        }
        
        public virtual void Show()
        {
            Root.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            Root.gameObject.SetActive(false);
        }

        public void AddChildComponent(Component component)
        {
            childComponents.Add(component);
            OnAddChildComponent(component);
        }

        protected virtual void OnAddChildComponent(Component component)
        {
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            if (rootResource is null)
            {
                Debug.LogWarning($"View for {Name} not created");
                return;
            }

            var parent = context is {ParentUiElement: { }} ? context.ParentUiElement.ContentHolder : location?.Root.transform;
            Root = Object.Instantiate(rootResource, parent);
            Root.name = $"[{GetType().Name}] {rootResource.name}";
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            childComponents.Clear();
            Object.Destroy(Root);
        }
    }
}