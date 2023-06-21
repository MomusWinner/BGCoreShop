using System;
using Configs;
using Core.ObjectsSystem;
using GameData;
using UI.View;
using Object = UnityEngine.Object;

namespace UI
{
    public class FadeView : UiElementView<FadeSetting, FadeComponent>
    {
    public FadeView(FadeSetting setting, IContext context, IDroppable parent) : base(setting, context, parent)
    {
    }

    public void SetAlfa(float alfa)
    {
        if (!Root)
            return;
        Root.GraphicMaskable.SetColorA(alfa);
    }

    protected override void OnAlive()
    {
        base.OnAlive();
        Root.GraphicMaskable.Initiate();
    }

    protected override void OnDrop()
    {
        try
        {
            Root.GraphicMaskable.Drop();
            Object.Destroy(Root.gameObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    }
}