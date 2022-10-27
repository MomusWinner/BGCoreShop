using BGCore.Game.Factories;
using Core.ObjectsSystem;
using UI.View;
using GameData;
using Game.Settings.UISettings;

namespace Game.UI
{
    public abstract class UiElement : BaseDroppable, IUiElement
    {
        public bool IsShown { get; }
        
        public string RootObjectResourcesPath { get; }

        protected UiElementView view;
        protected UISetting setting;

        protected UiElement(string name, UISetting setting, IContext context) : base(name)
        {
            this.setting = setting;
            RootObjectResourcesPath = setting.RootObjectPath;

            view = GeneralFactory.CreateItem<UiElementView, UiElement>(this, context);
        }

        public override void SetAlive()
        {
            base.SetAlive();
            Initialize();
            SetParent();
        }

        protected abstract void Initialize();
        protected abstract void SetParent();

        public void Show()
        {
            
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void Update<TUiAgs>(object sender, TUiAgs ags)
        {
            throw new System.NotImplementedException();
        }
    }
}