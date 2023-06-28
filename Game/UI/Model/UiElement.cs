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

        public override Transform Transform => view.Root.transform;

        public bool IsShown { get; protected set; }

        public List<IUiElement> ChildUiElements { get; set; }

        protected readonly TSetting setting;

        protected UiElement(TSetting setting, IContext context, IDroppable parent) :
            base(setting, context, parent)
        {
            this.setting = setting;
            AssignChild();
            if (setting.showOnAlive) Show(); else Hide();
        }
        
        public void Show(Action<object> onShow = null)
        {
            if (IsShown)
                return;
            IsShown = true;
            Scheduler.InvokeWhen(IsReadyForAliVe, () => OnShow(onShow));
        }

        public void Hide(Action<object> onHide = null)
        {
            if (!IsShown)
                return;
            IsShown = false;
            Scheduler.InvokeWhen(IsReadyForAliVe, () => OnHide(onHide));
        }

        public void Update<TUiAgs>(object sender, TUiAgs ags)
        {
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
        }

        protected virtual void OnHide(Action<object> onHide)
        {
            if (setting.showHideChildAffect)
                HideChild();
            view?.Hide();
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            view?.Drop();

            if (ChildUiElements is { })
            {
                foreach (var childUiElement in ChildUiElements)
                    childUiElement?.Drop();
            }

            ChildUiElements = null;
            view = null;
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
            ChildUiElements = new List<IUiElement>();
            for (var i = 0; i < setting.childUiElementSettings.Length; i++)
            {
                var uiSetting = setting.childUiElementSettings[i];
                if (!uiSetting)
                {
                    Debug.LogWarning($"{GetType()} have an empty config");
                    continue;
                }

                var uiElement = (IUiElement) uiSetting.GetInstance(context, this);
                ChildUiElements.Add(uiElement);
            }
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