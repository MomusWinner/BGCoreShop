using UI;
using UI.View;
using UnityEngine;

namespace UI
{
    public class FadeComponent : Component<FadeComponent>, IUIGraphicComponent
    {
        public IGraphicMaskable GraphicMaskable => graphicMaskable;
        
        [SerializeField] private GraphicMaskable graphicMaskable;
        
        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);

#if UNITY_EDITOR
        private void OnValidate()  => graphicMaskable?.OnValidate();
#endif
    }
}