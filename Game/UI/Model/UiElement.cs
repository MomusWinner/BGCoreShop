using System;
using System.Collections.Generic;
using System.Linq;
using BGCore.Game.Factories;
using Core.ObjectsSystem;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using GameLogic.GameData.Contexts;
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
            view = (TView) Activator.CreateInstance(typeof(TView), setting, context);
            AssignChild();
        }
        
        public void Show()
        {
            if(IsShown)
                return;
            IsShown = true;
            OnShow();
        }
        
        public void Hide()
        {
            if(!IsShown)
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
            base.OnAlive();
            view.SetAlive(location);
            SetContentHolder();
            ChildSetAlive();
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
            view.Show();
            ShowChild();
        }
        
        protected virtual void OnHide()
        {
            HideChild();
            view.Hide();
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
                childUiElement.SetAlive(location);
                view.AddChildComponent(childUiElement.RootComponent);
            }
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
            foreach (var uiElement in ChildUiElements)
                uiElement.Show();
        }

        private void HideChild()
        {
            foreach (var uiElement in ChildUiElements)
                uiElement.Hide();
        }
    }
}