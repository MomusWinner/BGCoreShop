using UnityEngine;

namespace UI.View
{
    public interface IUIGraphicComponent
    {
        Transform ContentHolder { get; }
        IGraphicMaskable GraphicMaskable { get; }
        void Show();
        void Hide();
    }

    public interface IGraphicMaskable
    {
        void Initiate();
        void SetColorA(float a);
        void AddMaskable(IGraphicMaskable maskable);
    }
}