using System.Threading.Tasks;
using Core.Locations.Model;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Locations
{
    internal static class LocationLoader
    {
        private static bool isLoadedStaticLocation;
        private static bool isLoadedDynamicLocation;

        private static Scene staticScene;
        private static Scene dynamicScene;

        public static async Task LoadBoth(Location statLocation, Location dynLocation)
        {
            void InnerLoadingScenes(object[] obj)
            {
                GEvent.Detach(GlobalEvents.LocationScenesUnloaded, InnerLoadingScenes);
                LoadingScenes(statLocation, dynLocation);
            }

            if (staticScene.isLoaded && statLocation.RootSceneName != staticScene.name ||
                dynamicScene.isLoaded && dynLocation.RootSceneName != dynamicScene.name)
            {
                GEvent.Attach(GlobalEvents.LocationScenesUnloaded, InnerLoadingScenes);

                await UnloadScenes();
            }
            else if (!staticScene.isLoaded && !dynamicScene.isLoaded)
            {
                LoadingScenes(statLocation, dynLocation);
            }

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
        }

        private static void LoadingScenes(Location statLocation, Location dynLocation)
        {
            var haveStatic = statLocation is { };
            var haveDynamic = dynLocation is { };
            if (!haveStatic)
            {
                Debug.LogWarning("Static location is null");
            }
            if (!haveDynamic)
            {
                Debug.LogWarning("Dynamic location is null");
            }

            if (!haveDynamic && !haveStatic)
            {
                Debug.LogError("Scene load failed");
                return;
            }
            
            var loadingStatic = SceneManager.LoadSceneAsync(statLocation.RootSceneName, LoadSceneMode.Additive);
            var loadingDynamic = SceneManager.LoadSceneAsync(dynLocation.RootSceneName, LoadSceneMode.Additive);

            loadingStatic.completed += _ =>
            {
                staticScene = SceneManager.GetSceneByName(statLocation.RootSceneName);
                isLoadedStaticLocation = true;
            };
            loadingDynamic.completed += _ =>
            {
                dynamicScene = SceneManager.GetSceneByName(dynLocation.RootSceneName);
                isLoadedDynamicLocation = true;
            };
        }
        
        private static async Task UnloadScenes()
        {
            var unloadingStatic = SceneManager.UnloadSceneAsync(staticScene);
            var unloadingDynamic = SceneManager.UnloadSceneAsync(dynamicScene);

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

            GEvent.Call(GlobalEvents.LocationScenesUnloaded);
        }
    }
}