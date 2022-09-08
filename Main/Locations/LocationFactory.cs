using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Main;
using Core.Main.Locations;
using UnityEngine.SceneManagement;

namespace Submodules.BGLogic.Main.Locations
{
    public static class LocationFactory
    {
        private static bool isLoadedStaticLocation;
        private static bool isLoadedDynamicLocation;
        public static async Task<(Location, Location)> CreateLocation(LocationSetting statSettings, LocationSetting dynSetting)
        {
            var loadingStatic = SceneManager.LoadSceneAsync(statSettings.sceneName, LoadSceneMode.Additive);
            var loadingDynamic =  SceneManager.LoadSceneAsync(dynSetting.sceneName, LoadSceneMode.Additive);

            loadingStatic.completed += _ => isLoadedStaticLocation = true;
            loadingDynamic.completed += _ => isLoadedDynamicLocation = true;
            
            await Task.Run(() =>
            {
                while (true)
                {
                    if (isLoadedStaticLocation)
                    {
                        break;
                    }
                }

                while (true)
                {
                    if (isLoadedDynamicLocation)
                    {
                        break;
                    }
                }
            });
            
            var statLocation = new Location(statSettings);
            var dynLocation = new Location(dynSetting);
            
            GEvent.Call(GlobalEvents.LocationLoaded, statLocation, dynLocation);

            return (statLocation, dynLocation);
        }

        public static void DropLocation(Location statLocation, Location dynamicLocation)
        {
            statLocation.Drop();
            dynamicLocation.Drop();

            var unloadingStatic = SceneManager.UnloadSceneAsync(statLocation.RootSceneName);
            var unloadingDynamic = SceneManager.UnloadSceneAsync(dynamicLocation.RootSceneName);
            
            unloadingStatic.completed += _ => isLoadedStaticLocation = false;
            unloadingDynamic.completed += _ => isLoadedDynamicLocation = false;
            
            GEvent.Call(GlobalEvents.LocationUnloaded);
        }
    }
}