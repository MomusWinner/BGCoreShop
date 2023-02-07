using System.Linq;
using Game.Settings;

namespace Core.Locations.Model
{
    public abstract class LocationSetting : ViewSetting
    {
        public BaseSetting[] childSettings;

        public T GetConfig<T>() where T: BaseSetting
        {
            return childSettings.FirstOrDefault(s => s is T) as T;
        }
        public BaseSetting[] locationObjectsSettings;
    }
}