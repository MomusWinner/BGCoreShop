using Game.Settings;
using UnityEngine;

namespace Core.Locations.Model
{
    public class LocationSetting : ViewSetting
    {
        public string SceneName => sceneName;

        [SerializeField] private string sceneName;
    }
}