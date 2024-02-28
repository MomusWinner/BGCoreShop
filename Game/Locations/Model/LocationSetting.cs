using System.Linq;
using Game.Settings;
using UnityEngine;

namespace Core.Locations.Model
{
    public abstract class LocationSetting : ViewSetting
    {
        public string SceneName => sceneName;

        [SerializeField] private string sceneName;
        public BaseSetting[] childSettings;

        public T GetConfig<T>() where T: BaseSetting
        {
            return childSettings.FirstOrDefault(s => s is T) as T;
        }
    }
}