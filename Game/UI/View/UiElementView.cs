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
        protected GameObject rootResource;

        protected UiElementView(UiElement uiElementParent, IContext ctx) : base(uiElementParent.Name)
        {
            ParentUiElement = uiElementParent;
            rootResource = Resources.Load<GameObject>(uiElementParent.RootObjectResourcesPath);
            context = ctx;
        }

        public void Initialize(Transform parent = null)
        {
            if (rootResource)
            {
                InnerInitialize(parent);
            }
        }

        public void SetParent(Transform parent)
        {
            if (Root)
            {
                Root.transform.SetParent(parent);
            }
        }

        protected virtual void InnerInitialize(Transform parent = null)
        {
            if (rootResource is null)
            {
                Debug.LogWarning($"View for {Name} not created");
                return;
            }
            
            Root = Object.Instantiate(rootResource, parent);
            Root.name = "[UI Element] " + rootResource.name;
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}