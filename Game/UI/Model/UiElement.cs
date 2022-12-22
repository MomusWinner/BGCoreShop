using BGCore.Game.Factories;
using Core.ObjectsSystem;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using GameLogic.GameData.Contexts;
using UnityEngine;

namespace Game.UI
{
    public abstract class UiElement : BaseDroppable, IUiElement
    {
        public abstract Transform ContentHolder { get; protected set; }

        public bool IsShown { get; private set; }

        public string RootObjectResourcesPath { get; }

        private IUiElement[] ChildUiElements { get; set; }


        protected Transform parent;
        protected readonly IContext context;
        protected UiElementView view;
        private readonly UISetting setting;


        protected UiElement(string name, UISetting setting, IContext context) : base(name)
        {
            this.context = context;
            this.setting = setting;
            RootObjectResourcesPath = setting.RootObjectPath;
            view = GeneralFactory.CreateItem<UiElementView, UiElement>(this, context);
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            Initialize();
            SetAliveChilds();
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

        protected override void OnDrop()
        {
            base.OnDrop();
            view.Drop();

            if (ChildUiElements is { })
            {
                foreach (var childUiElement in ChildUiElements)
                {
                    childUiElement.Drop();
                }
            }

            ChildUiElements = null;
            view = null;
        }

        protected abstract void Initialize();

        protected virtual void AssignChilds()
        {
            var childContext =
                ((UiContext) GeneralFactory.CreateItem<IContext, UISetting>(setting, context))?.SendParent(this);
            if (childContext != null)
            {
                childContext.AddContext(context.GetContext<GeneralContext>());
                ChildUiElements = new IUiElement[setting.childUiElementSettings.Length];

                for (var i = 0; i < ChildUiElements.Length; i++)
                {
                    ChildUiElements[i] =
                        GeneralFactory.CreateItem<IUiElement, UISetting>(setting.childUiElementSettings[i],
                            childContext);
                    childContext.SetSelf(ChildUiElements[i]);
                }
            }
        }

        private void SetAliveChilds()
        {
            if (ChildUiElements is null)
            {
                return;
            }

            foreach (var childUiElement in ChildUiElements)
            {
                childUiElement.SetAlive();
            }
        }
    }
}