namespace UI.View
{
    public interface IUIGraphicComponent
    {
        IGraphicMaskable GraphicMaskable { get; }
        void Show();
        void Hide();
    }

    public interface IGraphicMaskable
    {
        void Initiate();
        void Drop();
        void SetColorA(float a);
        void AddMaskable(IGraphicMaskable maskable);
    }
}