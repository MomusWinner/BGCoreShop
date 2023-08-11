using System;
using System.Collections.Generic;
using Core.ObjectsSystem;
using UI.View;

namespace Game.UI
{
    public interface IUiElement : IDroppable
    {
        IUIGraphicComponent RootComponent { get; }
        bool ViewALive { get; }
        List<IUiElement> ChildUiElements { get; set; }
        bool IsShown { get; }
        void Show(Action<object> onShowAction = null);
        void Hide(Action<object> onHideAction = null);
        T GetChild<T>() where T : IUiElement;
    }
}