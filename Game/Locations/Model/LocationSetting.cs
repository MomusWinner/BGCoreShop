using Game.Settings;
using UnityEngine;

namespace Core.Locations.Model
{
    public abstract class LocationSetting : ViewSetting
    {
        public string SceneName => sceneName;

        [SerializeField] private string sceneName;
    }
}