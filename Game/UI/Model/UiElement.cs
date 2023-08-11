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
        private int loadedChildCount;

        protected UiElement(TSetting setting, IContext context, IDroppable parent) :
            base(setting, context, parent)
        {
            this.setting = setting;
            AssignChild();
            if (setting.showOnAlive) Show();
            else Hide();
        }

        public void Show(Action<object> onShow = null)
        {
            if (IsShown)
                return;
            IsShown = true;
            Scheduler.InvokeWhen(() => Alive, () => OnShow(onShow));
        }

        public void Hide(Action<object> onHide = null)
        {
            if (!IsShown)
                return;
            IsShown = false;
            Scheduler.InvokeWhen(() => Alive, () => OnHide(onHide));
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
            ChildUiElements ??= new List<IUiElement>();
            TryCreateNextChild();
        }

        private void TryCreateNextChild()
        {
            if (setting.childUiElementSettings.Length > loadedChildCount)
            {
                var uiSetting = setting.childUiElementSettings[loadedChildCount];
                if (!uiSetting)
                {
                    Debug.LogWarning($"{GetType()} have an empty config");
                    return;
                }

                var child = (IUiElement) setting.childUiElementSettings[loadedChildCount].GetInstance(context, this);
                child.OnLively += ChildOnOnLively;
                ChildUiElements.Add(child);
            }
        }

        private void ChildOnOnLively(IDroppable obj)
        {
            obj.OnLively -= ChildOnOnLively;
            loadedChildCount++;
            TryCreateNextChild();
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