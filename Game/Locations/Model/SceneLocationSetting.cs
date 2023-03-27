using UnityEngine;

namespace Core.Locations.Model
{
    [CreateAssetMenu(menuName = "Game/Settings/"+ nameof(SceneLocationSetting), fileName = nameof(SceneLocationSetting))]
    public class SceneLocationSetting : LocationSetting
    {
        public string SceneName => sceneName;

        [SerializeField] private string sceneName;
    }
}