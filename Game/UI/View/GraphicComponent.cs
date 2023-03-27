using Game;
using MonoComponents;
using UnityEngine;

namespace UI.View
{
    public abstract class GraphicComponent<TComponent> : Component<TComponent>, IUIGraphicComponent where TComponent : Component
    {
        public Transform ContentHolder => contentHolder ? contentHolder : transform;
        public IGraphicMaskable GraphicMaskable => graphicMaskable;

        [SerializeField] private GraphicMaskable graphicMaskable;
        [SerializeField] private Transform contentHolder;
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}