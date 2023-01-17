namespace UI.View
{
    public interface IUIGraphicComponent
    {
        IGraphicMaskable GraphicMaskable { get; }
    }

    public interface IGraphicMaskable
    {
        void Initiate();
        void AddMaskable(IGraphicMaskable maskable);
    }
}