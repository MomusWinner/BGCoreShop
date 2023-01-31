using System;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace MonoComponents
{
    [Serializable]
    public class Maskable : IGraphicMaskable
    {
        public MaskableGraphic GraphicElement
        {
            get => uiElement;
            set => uiElement = value;
        }
        
        [SerializeField, HideInInspector] private string name;
        [SerializeField] private MaskableGraphic uiElement;
        
        private float fadeValue;
        private const float unFadeValue = 0f;
        public void Initiate()
        {
            fadeValue = uiElement.color.a;
        }
        
        public void OnValidate()
        {
            if(uiElement)
                name = uiElement.name;
        }

        public void SetColorA(float a)
        {
            var color = uiElement.color;
            color[3] = Mathf.Lerp(unFadeValue, fadeValue, a);
            uiElement.color = color;
        }

        public void AddMaskable(IGraphicMaskable maskable)
        {
        }
    }
}