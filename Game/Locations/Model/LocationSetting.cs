using Game.Settings;
using UnityEngine;

namespace Core.Locations.Model
{
    [CreateAssetMenu(menuName = "Game/Settings/LocationSettings", fileName = "LocationSetting", order = 0)]
    public class LocationSetting : ViewSetting
    {
        public string SceneName => sceneName;

        [SerializeField] private string sceneName;
    }
}