using Core.ObjectsSystem;
using Game.Settings.UISettings;
using GameData;

namespace Configs
{
    public abstract class FadeSetting : UISetting
    {
        public float fadeSpeed;

        public override IDroppable GetInstance<TContext>(TContext context, IDroppable parent)
        {
            return GetFadeInstance(context, parent);
        }

        public override BaseDroppable GetViewInstance<TContext>(TContext context, IDroppable parent)
        {
            return GetFadeViewInstance(context, parent);
        }

        protected abstract IDroppable GetFadeInstance<TContext>(TContext context, IDroppable parent)
            where TContext : IContext;
        
        protected abstract BaseDroppable GetFadeViewInstance<TContext>(TContext context, IDroppable parent)
            where TContext : IContext;
    }
}