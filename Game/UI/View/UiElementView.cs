using Core.ObjectsSystem;
using Game.UI;
using GameData;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.View
{
    public abstract class UiElementView : BaseDroppable
    {
        public GameObject Root { get; protected set; }
        protected UiElement ParentUiElement { get; }

        protected readonly IContext context;
        protected readonly GameObject rootResource;
        protected Transform parent;

        protected UiElementView(UiElement uiElementParent, IContext ctx) : base(uiElementParent.Name)
        {
            ParentUiElement = uiElementParent;
            rootResource = Resources.Load<GameObject>(uiElementParent.RootObjectResourcesPath);
            context = ctx;
        }

        public void Initialize(Transform parent = null)
        {
            this.parent = parent;
            SetAlive();
        }

        public void SetParent(Transform parent)
        {
            if (Root)
            {
                this.parent = parent;
                Root.transform.SetParent(parent);
            }
        }

        public virtual void Show()
        {
            Debug.Log("SSSSSSSSSSSSSSSSS");
            Root.SetActive(true);
        }

        public virtual void Hide()
        {
            Root.SetActive(false);
        }
        
        protected override void OnAlive()
        {
            base.OnAlive();
            if (rootResource is null)
            {
                Debug.LogWarning($"View for {Name} not created");
                return;
            }
            
            Root = Object.Instantiate(rootResource, parent);
            Root.name = $"[{GetType().Name}] {rootResource.name}";
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
        }
    }
}