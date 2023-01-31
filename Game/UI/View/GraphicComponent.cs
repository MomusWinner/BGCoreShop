using MonoComponents;
using UnityEngine;

namespace UI.View
{
    public abstract class GraphicComponent<TComponent> : Component<TComponent>, IUIGraphicComponent where TComponent : Component
    {
        public IGraphicMaskable GraphicMaskable => graphicMaskable;

        [SerializeField] private GraphicMaskable graphicMaskable;
        
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