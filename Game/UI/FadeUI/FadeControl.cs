using System;
using Configs;
using Core.Entities.Loopables;
using Core.LoopSystem;
using UnityEngine;

namespace UI
{
    public class FadeControl : ControlLoopable
    {
        public Action<float> OnUnFaded { get; set; }
        public Action<float> OnFadeUpdate { get; set; }
        public Action<float> OnFaded { get; set; }

        private float target;
        private float value = 1;
        private bool isFaded;
        private bool isUnFaded;
        
        private readonly FadeSetting setting;

        private const float fadeValue = 1f;
        private const float unFadeValue = 0f;

        public FadeControl(FadeSetting setting)
        {
            this.setting = setting;
        }

        public void Fade()
        {
            UnFaded();
            target = fadeValue;
            Play();
        }

        public void UnFade()
        {
            Faded();
            target = unFadeValue;
            Play();
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            LoopOn(Loops.Update, OnUpdate);
        }

        private void OnUpdate()
        {
            if (Mathf.Abs(value - target) > Mathf.Epsilon)
            {
                value = Mathf.MoveTowards(value, target, setting.fadeSpeed * Time.deltaTime);
                OnFadeUpdate?.Invoke(value);
                return;
            }

            if (!isUnFaded && target.Equals(unFadeValue))
            {
                UnFaded();
                return;
            }
            
            if (!isFaded && target.Equals(fadeValue))
            {
                Faded();
            }
        }

        private void UnFaded()
        {
            isUnFaded = true;
            OnUnFaded?.Invoke(value);
            Pause();
            isFaded = false;
            return;
        }

        private void Faded()
        {
            isFaded = true;
            OnFaded?.Invoke(value);
            Pause();
            isUnFaded = false;
        }
    }
}