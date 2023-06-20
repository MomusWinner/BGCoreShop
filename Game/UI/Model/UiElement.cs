using System;
using System.Collections.Generic;
using System.Linq;
using BGCore.Game.Factories;
using Core;
using Core.ObjectsSystem;
using Game.Contexts;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using UnityEngine;

namespace Game.UI
{
    public abstract class UiElement<TView, TSetting, TComponent> : BaseDroppable, IUiElement
        where TView : UiElementView<TSetting, TComponent>
        where TSetting : UISetting
        where TComponent : Component, IUIGraphicComponent
    {
        public IUIGraphicComponent RootComponent => view.Root;
        public Transform ContentHolder { get; protected set; }

        public bool IsShown { get; protected set; }

        protected List<IUiElement> ChildUiElements { get; set; }
        protected readonly UiContext uiContext;

        protected TView view;
        protected readonly TSetting setting;

        protected UiElement(TSetting setting, UiContext context)
        {
            uiContext = context;
            uiContext?.SetSelf(this);
            this.setting = setting;
#if !UNITY_WEBGL
            view ??= (TView) Activator.CreateInstance(typeof(TView), setting, context);
            AssignChild();
#endif
        }

        public void Show()
        {
            if (IsShown)
                return;
            IsShown = true;
            OnShow();
        }

        public void Hide()
        {
            if (!IsShown)
                return;
            IsShown = false;
            OnHide();
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

        protected override void OnAlive()
        {
            Alive = false;
            base.OnAlive();
            view.SetAlive(parent);
            SetContentHolder();
            ChildSetAlive();
            Scheduler.InvokeWhen(() => ChildUiElements.All(e => e.Alive) || ChildUiElements.Count is 0,
                () =>
                {
                    Alive = true;
                });
            IsShown = setting.showOnAlive;
            if (setting.showOnAlive)
            {
                view.Show();
                return;
            }

            view.Hide();
        }

        protected virtual void SetContentHolder()
        {
            ContentHolder = view.Root.transform;
        }

        protected virtual void OnShow()
        {
            view?.Show();
            ShowChild();
        }

        protected virtual void OnHide()
        {
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

        protected void ChildSetAlive()
        {
            if (ChildUiElements is null)
                return;

            foreach (var childUiElement in ChildUiElements)
            {
                childUiElement.SetAlive(parent);
                view.AddChildComponent(childUiElement.RootComponent);
            }
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
            foreach (var uiSetting in setting.childUiElementSettings)
            {
                var childContext = new UiContext().SendParent(this);
                childContext.AddContext(uiContext.GetContext<MainContext>());
                ChildUiElements.Add((IUiElement) Factory.CreateItem(uiSetting, childContext));
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