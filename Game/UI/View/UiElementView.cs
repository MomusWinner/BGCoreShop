using Core.Locations.Model;
using Core.ObjectsSystem;
using Game;
using Game.Settings.UISettings;
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
            rootResource = Resources.Load<TComponent>(setting.rootObjectPath);
            context = ctx;
        }
        
        public void Show()
        {
            Root.Show();
        }

        public void Hide()
        {
            Root.Hide();
        }

        public void AddChildComponent(IUIGraphicComponent graphic)
        {
            Root.GraphicMaskable.AddMaskable(graphic.GraphicMaskable);
            OnAddChildComponent(graphic);
        }

        protected virtual void OnAddChildComponent(IUIGraphicComponent component)
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
            
            var parentUiTransform = context is {ParentUiElement: { }} ? context.ParentUiElement.ContentHolder : parent is Location location ? location.Root.transform : null;
            Root = Object.Instantiate(rootResource, parentUiTransform);
            Root.name = $"[{GetType().Name}] {rootResource.name}";
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}