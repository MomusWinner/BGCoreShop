using Core.Main.Settinngs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Main.Locations
{
    [CreateAssetMenu(menuName = "Game/Settings/LocationSettings", fileName = "LocationSetting", order = 0)]
    public class LocationSetting : BaseSetting
    {
        public string RootObjectPath => rootObjectPath;
        public string SceneName => sceneName;
        
        [SerializeField] private string sceneName;
        [SerializeField] private Object rootObject;
        [SerializeField, HideInInspector] private string rootObjectPath;

        private void OnValidate()
        {
            rootObjectPath = Utilities.GetValidPathToResource(rootObject);
        }
    }
}