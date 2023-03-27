using System.Linq;
using Game.Settings;
using UnityEngine;

namespace Core.Locations.Model
{
    [CreateAssetMenu(menuName = "Game/Settings/"+ nameof(LocationSetting), fileName = nameof(LocationSetting))]
    public class LocationSetting : ViewSetting
    {
        public string SceneName => sceneName;
        
        public BaseSetting[] childSettings;

        [SerializeField] private string sceneName;

        public T GetConfig<T>() where T: BaseSetting
        {
            return childSettings.FirstOrDefault(s => s is T) as T;
        }
    }
}