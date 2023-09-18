using Configs;
using Core.Locations.View;
using Core.ObjectsSystem;
using Game.Locations;
using Game.UI;
using UnityEngine;

namespace Core.Locations.Model
{
    [CreateAssetMenu(menuName = "Game/Setting/" + nameof(SceneLocation))]
    public class SceneLocationSetting : LocationSetting
    {
        public string sceneName;
        public FadeSetting startFade;
        public float fadeDelay;

        public override IDroppable GetInstance<TContext>(TContext context, IDroppable parent)
        {
            return new SceneLocation(this, context, parent);
        }

        public override BaseDroppable GetViewInstance<TContext>(TContext context, IDroppable parent)
        {
            return new LocationView(this, context, parent);
        }
    }
}