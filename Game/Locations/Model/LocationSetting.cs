using System.Linq;
using Configs;
using Core.Locations.View;
using Core.ObjectsSystem;
using Game.Locations;
using Game.Settings;
using UnityEngine;

namespace Core.Locations.Model
{
    public abstract class LocationSetting : ViewSetting
    {

        public BaseSetting[] childSettings;

        public string sceneName;
        public FadeSetting startFade;
        public float fadeDelay;

        public T GetConfig<T>() where T : BaseSetting
        {
            return childSettings.FirstOrDefault(s => s is T) as T;
        }

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