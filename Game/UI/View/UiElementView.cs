using System.Linq;
using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.Settings.UISettings;
using Game.UI;
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
            Debug.Log($"Construct {base.setting.name} -> {Name}");
        }

        public void Show()
        {
            if (Root)
                Root.Show();
            OnShow();
        }

        public void Hide()
        {
            if (Root)
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

        protected virtual bool IsReadyForALive()
        {
            return base.parent is {Alive: true};
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}