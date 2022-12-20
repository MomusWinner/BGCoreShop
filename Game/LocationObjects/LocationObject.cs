using Core.ObjectsSystem;

namespace Game.LocationObjects
{
    public abstract class LocationObject<TView> : BaseDroppable where TView : BaseDroppable
    {
        protected TView view;
        protected LocationObject(string name) : base(name)
        {
        }

        public override void SetAlive()
        {
            base.SetAlive();
            view?.SetAlive();
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            view?.Drop();
            view = null;
        }
    }
}