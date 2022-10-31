using System;
using BGCore.Game.Factories;
using Core.ObjectsSystem;
using FluffyUnderware.DevTools.Extensions;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using GameLogic.GameData;
using UnityEngine;

namespace Game.UI
{
    public abstract class UiElement : BaseDroppable, IUiElement
    {
        private Action OnInitialize { get; set; }
        
        public GameObject Root => view.Root;
        
        public bool IsShown { get; }
        
        public string RootObjectResourcesPath { get; }

        protected Transform parent;
        protected readonly Context context;
        protected UiElementView view;

        private IUiElement[] ChildUiElements { get; set; }
        private UISetting setting;

        
        protected UiElement(string name, UISetting setting, IContext context) : base(name)
        {
            this.context = (Context)context;
            this.setting = setting;
            RootObjectResourcesPath = setting.RootObjectPath;
            
            view = GeneralFactory.CreateItem<UiElementView, UiElement>(this, context);
            
            var childContext = new Context(context);
            childContext.SetUiElementParent(this);

            if (setting.childUiElementSettings is { } childSettings)
            {
                ChildUiElements = new IUiElement[childSettings.Length];
                for (var i = 0; i < childSettings.Length; i++)
                {
                    ChildUiElements[i] = GeneralFactory.CreateItem<IUiElement, UISetting>(childSettings[i], childContext);
                }
            }
        }

        public override void SetAlive()
        {
            base.SetAlive();

            Initialize();
            SetParent();
            
            ChildUiElements?.ForEach(e=>e.SetAlive());
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            view.Drop();
            ChildUiElements?.ForEach(e=>e.Drop());
            ChildUiElements = null;
            view = null;
        }

        protected virtual void Initialize()
        {
            parent = context.CurrentUiParent?.Root.transform;
        }
        protected abstract void SetParent();

        public void Show()
        {
        }

        public void Hide()
        {
        }

        public void Update<TUiAgs>(object sender, TUiAgs ags)
        {
        }
    }
}