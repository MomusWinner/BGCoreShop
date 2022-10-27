using System.Threading.Tasks;
using System.Xml.Schema;
using Core.Locations.Model;
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
                dynamicScene.isLoaded && dynLocation.RootSceneName != dynamicScene.name ||
                !staticScene.isLoaded || !dynamicScene.isLoaded)
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
                isLoadedStaticLocation = true;
            }
            else
            {
                var loadingStatic = SceneManager.LoadSceneAsync(statLocation.RootSceneName, LoadSceneMode.Additive);
                
                loadingStatic.completed += _ =>
                {
                    staticScene = SceneManager.GetSceneByName(statLocation.RootSceneName);
                    isLoadedStaticLocation = true;
                };
            }
            if (!haveDynamic)
            {
                Debug.LogWarning("Dynamic location is null");
                isLoadedDynamicLocation = true;
            }
            else
            {
                var loadingDynamic = SceneManager.LoadSceneAsync(dynLocation.RootSceneName, LoadSceneMode.Additive);

                loadingDynamic.completed += _ =>
                {
                    dynamicScene = SceneManager.GetSceneByName(dynLocation.RootSceneName);
                    isLoadedDynamicLocation = true;
                };
            }

            if (!haveDynamic && !haveStatic)
            {
                Debug.LogError("Scene load failed");
            }
        }
        
        private static async Task UnloadScenes()
        {
            if (staticScene.isLoaded)
            {
                var unloadingStatic = SceneManager.UnloadSceneAsync(staticScene);
                
                unloadingStatic.completed += _ => isLoadedStaticLocation = false;
            }
            else
            {
                isLoadedStaticLocation = false;
            }

            if (dynamicScene.isLoaded)
            {
                var unloadingDynamic = SceneManager.UnloadSceneAsync(dynamicScene);

                unloadingDynamic.completed += _ => isLoadedDynamicLocation = false;
            }
            else
            {
                isLoadedDynamicLocation = false;
            }

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