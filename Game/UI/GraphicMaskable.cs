using System;
using System.Collections.Generic;
using System.Linq;
using UI.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoComponents
{
    [Serializable]
    public class GraphicMaskable : IGraphicMaskable
    {
        [SerializeField] private List<Maskable> maskables;
        
        private List<IGraphicMaskable> cachedMaskable = new List<IGraphicMaskable>();

        public void Initiate()
        {
            foreach (var maskable in maskables.Where(maskable => !cachedMaskable.Contains(maskable)))
            {
                cachedMaskable.Add(maskable);
                maskable.Initiate();
            }
        }
        
        public void SetColorA(float a)
        {
            foreach (var fadable in cachedMaskable)
                fadable.SetColorA(a);
        }

        public void AddMaskable(IGraphicMaskable maskable)
        {
            if(maskable is null)
                return;
            maskable.Initiate();
            cachedMaskable.Add(maskable);
        }

        public void OnValidate()
        {
            foreach (var fadable in maskables) 
                fadable?.OnValidate();
        }

    }
}