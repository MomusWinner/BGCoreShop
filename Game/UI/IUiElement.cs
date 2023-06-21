using System;
using Core.ObjectsSystem;
using UI.View;

namespace Game.UI
{
    public interface IUiElement : IDroppable
    {
        IUIGraphicComponent RootComponent { get; }
        bool IsShown { get; }
        void Show(Action<object> onShowAction = null);
        void Hide(Action<object> onHideAction = null);
        void Update<TUiAgs>(object sender, TUiAgs ags);
        T GetChild<T>() where T : IUiElement;
    }
}