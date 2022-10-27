using Core.ObjectsSystem;

namespace Game.UI
{
    public interface IUiElement : IDroppable
    {
        bool IsShown { get; }
        void Show();
        void Hide();
        void Update<TUiAgs>(object sender, TUiAgs ags);
    }
}