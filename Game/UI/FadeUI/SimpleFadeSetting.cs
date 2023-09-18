using Core.ObjectsSystem;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Game/Settings/UI Settings/" + nameof(Fade))]
    public class SimpleFadeSetting : FadeSetting
    {
        protected override IDroppable GetFadeInstance<TContext>(TContext context, IDroppable parent)
        {
            return new Fade(this, context, parent);
        }

        protected override BaseDroppable GetFadeViewInstance<TContext>(TContext context, IDroppable parent)
        {
            return new FadeView(this, context, parent);
        }
    }
}