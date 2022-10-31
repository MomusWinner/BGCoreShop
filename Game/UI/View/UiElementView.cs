using Core.ObjectsSystem;
using Game.UI;
using GameData;
using UnityEngine;

namespace UI.View
{
    public abstract class UiElementView : BaseDroppable
    {
        public GameObject Root { get; protected set; }
        protected UiElement ParentUiElement { get; }

        protected readonly IContext context;
        private readonly GameObject rootResource;

        protected UiElementView(UiElement uiElementParent, IContext ctx) : base(uiElementParent.Name)
        {
            ParentUiElement = uiElementParent;
            rootResource = Resources.Load<GameObject>(uiElementParent.RootObjectResourcesPath);
            context = ctx;
        }

        public void Initialize()
        {
            if (rootResource)
            {
                InnerInitialize();
            }
        }

        public void SetParent(Transform parent)
        {
            if (Root)
            {
                Root.transform.SetParent(parent);
            }
        }

        protected virtual void InnerInitialize()
        {
            Root = Object.Instantiate(rootResource);
            Root.name = "[UI Element] " + rootResource.name;
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}