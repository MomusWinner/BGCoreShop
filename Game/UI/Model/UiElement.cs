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

        public bool IsShown { get; private set; }

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
        
        public virtual void Show()
        {
            IsShown = true;
            view.Show();
        }

        public virtual void Hide()
        {
            IsShown = false;
            view.Hide();
        }

        public void Update<TUiAgs>(object sender, TUiAgs ags)
        {
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
        }

        protected virtual void SetContentHolder()
        {
            ContentHolder = view.Root.transform;
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
        
        private void AssignChild()
        {
            ChildUiElements = new List<IUiElement>();
            foreach (var uiSetting in setting.childUiElementSettings)
            {
                var childContext = new UiContext().SendParent(this);
                childContext.AddContext(uiContext.GetContext<MainContext>());
                ChildUiElements.Add((IUiElement) Factory.CreateItem(uiSetting, childContext));
            }
        }
    }
}