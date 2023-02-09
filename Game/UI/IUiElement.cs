using Core.ObjectsSystem;
using UI.View;
using UnityEngine;

namespace Game.UI
{
    public interface IUiElement : IDroppable
    {
        IUIGraphicComponent RootComponent { get; }
        Transform  ContentHolder { get; }
        bool IsShown { get; }
        void Show();
        void Hide();
        void Update<TUiAgs>(object sender, TUiAgs ags);
        T GetChild<T>() where T : IUiElement;
    }
}