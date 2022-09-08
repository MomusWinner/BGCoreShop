using System.IO;
using System.Threading.Tasks;
using Core.Main;
using Core.Main.Locations;
using UnityEngine.SceneManagement;

namespace Submodules.BGLogic.Main.Locations
{
    internal static class LocationLoader
    {
        private static bool isLoadedStaticLocation;
        private static bool isLoadedDynamicLocation;

        private static string sceneStaticName;
        private static string sceneDynamicName;

        public static async Task<(Location, Location)> LoadBoth(Location statLocation, Location dynLocation)
        {
            if (!string.IsNullOrEmpty(sceneDynamicName) &&
                !string.IsNullOrEmpty(sceneStaticName) &&
                (statLocation.RootSceneName != sceneStaticName ||
                 dynLocation.RootSceneName != sceneDynamicName))
            {
                sceneDynamicName = dynLocation.RootSceneName;
                sceneStaticName = statLocation.RootSceneName;

                await UnloadScenes(LocationSection.StatLocation, LocationSection.DynLocation);
            }
            
            sceneDynamicName = dynLocation.RootSceneName;
            sceneStaticName = statLocation.RootSceneName;

            var loadingStatic = SceneManager.LoadSceneAsync(statLocation.RootSceneName, LoadSceneMode.Additive);
            var loadingDynamic = SceneManager.LoadSceneAsync(dynLocation.RootSceneName, LoadSceneMode.Additive);

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

            GEvent.Call(GlobalEvents.BothLocationLoaded, statLocation, dynLocation);
            return (statLocation, dynLocation);
        }

        public static async void DropBoth(Location statLocation, Location dynamicLocation)
        {
            statLocation.Drop();
            dynamicLocation.Drop();
        }

        private static async Task UnloadScenes(Location statLocation, Location dynamicLocation)
        {
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

            GEvent.Call(GlobalEvents.BothLocationUnloaded, statLocation, dynamicLocation);
        }
    }
}