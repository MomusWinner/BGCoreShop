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

        protected IUiElement[] ChildUiElements { get; set; }
        protected readonly UiContext uiContext;
        
        protected TView view;
        private readonly UISetting setting;


        protected UiElement(UISetting setting, UiContext context)
        {
            uiContext = context;
            uiContext?.SetSelf(this);
            this.setting = setting;
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

            AssignChilds();
            view.SetAlive(location);
            ContentHolder = view.Root.transform;
            SetAliveChilds();
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

        protected virtual void AssignChilds()
        {
            ChildUiElements = new IUiElement[setting.childUiElementSettings.Length];
            for (var i = 0; i < ChildUiElements.Length; i++)
            {
                var childContext = new UiContext().SendParent(this);
                childContext.AddContext(uiContext.GetContext<MainContext>());
                ChildUiElements[i] = (IUiElement) Factory.CreateItem(setting.childUiElementSettings[i], childContext);
            }
        }

        private void SetAliveChilds()
        {
            if (ChildUiElements is null)
                return;

            foreach (var childUiElement in ChildUiElements)
            {
                childUiElement.SetAlive(location);
                view.AddChildComponent(childUiElement.RootComponent);
            }
        }
    }
}