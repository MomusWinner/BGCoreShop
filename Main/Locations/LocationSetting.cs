using Core.Main.Settinngs;
using UnityEngine;

namespace Core.Main.Locations
{
    [CreateAssetMenu(menuName = "Game/Settings/LocationSettings", fileName = "LocationSetting", order = 0)]
    public class LocationSetting : BaseSetting
    {
        public string sceneName;
        public Object rootObject;
    }
}