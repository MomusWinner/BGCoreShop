using System;
using Configs;
using Core;
using Core.ObjectsSystem;
using Game.UI;
using GameData;
using UnityEngine;

namespace UI
{
    public class Fade : UiElement<FadeView, FadeSetting, FadeComponent>
    {
    public bool IsActive { get; private set; }
    private readonly FadeControl fadeControl;

    public Fade(FadeSetting setting, IContext context, IDroppable parent) : base(setting, context, parent)
    {
        fadeControl = new FadeControl(setting);
    }

    protected override void OnAlive()
    {
        base.OnAlive();
        fadeControl.OnFadeUpdate += view.SetAlfa;
        fadeControl.SetAlive();
    }

    protected override void OnShow(Action<object> onShow)
    {
        void OnFaded(float value)
        {
            fadeControl.OnFaded -= OnFaded;
            onShow?.Invoke(value);
        }

        if (IsActive)
            return;

        IsActive = true;
        base.OnShow(onShow);
        
        Scheduler.InvokeWhen(()=> Alive, () =>
        {
            fadeControl.OnFaded += OnFaded;
            fadeControl.Fade();
        });
    }

    protected override void OnHide(Action<object> onHide)
    {
        void OnUnFaded(float value)
        {
            base.OnHide(onHide);
            fadeControl.OnUnFaded -= OnUnFaded;
            onHide?.Invoke(value);
        }

        if (!IsActive)
            return;

        fadeControl.OnUnFaded += OnUnFaded;
        fadeControl.UnFade();
        IsActive = false;
    }

    public void SetImage(Sprite sprite)
    {

    }

    protected override void OnDrop()
    {
        fadeControl.OnFadeUpdate -= view.SetAlfa;
        fadeControl.Drop();
        view.Drop();
        base.OnDrop();
    }
    }
}