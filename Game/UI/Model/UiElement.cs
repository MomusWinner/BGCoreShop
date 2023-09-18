using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.ObjectsSystem;
using Game.LocationObjects;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using UnityEngine;

namespace Game.UI
{
    public abstract class UiElement<TView, TSetting, TComponent> :
        LocationObject<TView, TSetting, TComponent>, IUiElement
        where TView : UiElementView<TSetting, TComponent>
        where TSetting : UISetting
        where TComponent : Component, IUIGraphicComponent
    {
        public IUIGraphicComponent RootComponent => view.Root;
        public bool ViewALive => view.Alive;

        public override Transform Transform => view.Root.transform;

        public bool IsShown { get; protected set; }

        public List<IUiElement> ChildUiElements { get; set; }

        protected readonly TSetting setting;

        protected UiElement(TSetting setting, IContext context, IDroppable parent) :
            base(setting, context, parent)
        {
            this.setting = setting;
        }

        public void Show(Action<object> onShow = null)
        {
            if (IsShown)
                return;
            IsShown = true;
            OnShow(onShow);
        }

        public void Hide(Action<object> onHide = null)
        {
            if (!IsShown)
                return;
            IsShown = false;
            OnHide(onHide);
        }

        public T GetChild<T>() where T : IUiElement
        {
            foreach (var child in ChildUiElements)
            {
                if (child is T validChild)
                    return validChild;
                validChild = child.GetChild<T>();
                if (validChild is { })
                    return validChild;
            }

            return default;
        }

        public TUiElement GetElement<TUiElement>()
        {
            return (TUiElement) ChildUiElements.FirstOrDefault(e => e is TUiElement);
        }

        protected virtual void OnShow(Action<object> onShow)
        {
            view?.Show();
            if (setting.showHideChildAffect)
                ShowChild();
            onShow?.Invoke(this);
        }

        protected virtual void OnHide(Action<object> onHide)
        {
            if (setting.showHideChildAffect)
                HideChild();
            view?.Hide();
            onHide?.Invoke(this);
        }


        protected override void OnAlive()
        {
            base.OnAlive();
            AssignChild();

            foreach (var ui in ChildUiElements)
                ui.SetAlive();
            if (setting.showOnAlive) Show();
            else Hide();
        }

        protected override void OnDrop()
        {
            DropChild();
            base.OnDrop();
        }

        protected void ChildSetDrop(IUiElement element)
        {
            if (ChildUiElements is null)
                return;
            view.RemoveChildComponent(element.RootComponent);
            element.Drop();
            ChildUiElements.Remove(element);
        }


        protected void AssignChild()
        {
            ChildUiElements ??= new List<IUiElement>();
            foreach (var uiSetting in setting.childUiElementSettings)
                ChildUiElements.Add((IUiElement) uiSetting.GetInstance(context, this));
        }

        private void DropChild()
        {
            if (ChildUiElements is { })
            {
                foreach (var childUiElement in ChildUiElements)
                    childUiElement?.Drop();
            }

            ChildUiElements.Clear();
        }

        private void ShowChild()
        {
            if (ChildUiElements is null)
                return;
            foreach (var uiElement in ChildUiElements)
                uiElement.Show();
        }

        private void HideChild()
        {
            if (ChildUiElements is null)
                return;
            foreach (var uiElement in ChildUiElements)
                uiElement.Hide();
        }
    }
}