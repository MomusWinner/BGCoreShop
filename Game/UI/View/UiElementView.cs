using Core.ObjectsSystem;
using Game.Settings.UISettings;
using GameData;
using GameLogic.Views;
using UnityEngine;

namespace UI.View
{
    public abstract class UiElementView<TSetting, TComponent> : LocationObjectView<TSetting, TComponent>
        where TComponent : Component, IUIGraphicComponent
        where TSetting : UISetting
    {
        protected readonly IContext context;
        
        protected UiElementView(TSetting setting, IContext ctx, IDroppable parent) : base(setting, parent)
        {
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
        
        protected virtual void OnShow()
        {
        }
        
        protected virtual void OnHide()
        {
        }

    }
}