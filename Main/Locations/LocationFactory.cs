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
            var loadingStatic = SceneManager.LoadSceneAsync(statSettings.SceneName, LoadSceneMode.Additive);
            var loadingDynamic =  SceneManager.LoadSceneAsync(dynSetting.SceneName, LoadSceneMode.Additive);

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

        public static async Task DropLocation(Location statLocation, Location dynamicLocation)
        {
            statLocation.Drop();
            dynamicLocation.Drop();

            var unloadingStatic = SceneManager.UnloadSceneAsync(statLocation.RootSceneName);
            var unloadingDynamic = SceneManager.UnloadSceneAsync(dynamicLocation.RootSceneName);
            
            unloadingStatic.completed += _ => isLoadedStaticLocation = false;
            unloadingDynamic.completed += _ => isLoadedDynamicLocation = false;
            
            await Task.Run(() =>
            {
                while (true)
                {
                    if (!isLoadedStaticLocation)
                    {
                        break;
                    }
                }

                while (true)
                {
                    if (!isLoadedDynamicLocation)
                    {
                        break;
                    }
                }
            });

            
            GEvent.Call(GlobalEvents.LocationUnloaded);
        }
    }
}