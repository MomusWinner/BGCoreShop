using System;
using System.Collections.Generic;
using Core.ObjectsSystem;
using UI.View;

namespace Game.UI
{
    public interface IUiElement : IDroppable
    {
        IUIGraphicComponent RootComponent { get; }
        List<IUiElement> ChildUiElements { get; set; }
        bool IsShown { get; }
        void Show(Action<object> onShowAction = null);
        void Hide(Action<object> onHideAction = null);
        void Update<TUiAgs>(object sender, TUiAgs ags);
        T GetChild<T>() where T : IUiElement;
    }
}