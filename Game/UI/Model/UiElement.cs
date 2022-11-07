using System;
using System.Collections.Generic;
using BGCore.Game.Factories;
using Core.ObjectsSystem;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using UnityEngine;

namespace Game.UI
{
    public abstract class UiElement : BaseDroppable, IUiElement, IContext
    {
        private Action OnInitialize { get; set; }

        public GameObject Root => view.Root;

        public bool IsShown { get; }

        public string RootObjectResourcesPath { get; }

        protected IUiElement[] ChildUiElements { get; set; }


        protected Transform parent;
        protected readonly IContext context;
        protected UiElementView view;
        protected readonly UISetting setting;


        protected UiElement(string name, UISetting setting, IContext context) : base(name)
        {
            this.context = context;
            this.setting = setting;
            RootObjectResourcesPath = setting.RootObjectPath;

            view = GeneralFactory.CreateItem<UiElementView, UiElement>(this, context);

            AssignChilds();
        }

        public override void SetAlive()
        {
            base.SetAlive();

            Initialize();
            SetParent();

            SetAliveChilds();
        }

        public void Show()
        {
        }

        public void Hide()
        {
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
        protected abstract void AssignChilds();

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

        protected abstract void SetParent();

        public TType GetContext<TType>() where TType : IContext
        {
            return this is TType result ? result : default;
        }

        public void AddContext<TType>(IContext context) where TType : IContext
        {
            if (context is IUiElement uiElement)
            {
                parent = uiElement.Root.transform;
            }
        }

        public void RemoveContext<TType>(IContext context)where TType : IContext
        {
            throw new NotImplementedException();
        }
    }
}