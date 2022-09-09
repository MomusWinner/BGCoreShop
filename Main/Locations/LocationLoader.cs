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

        private static Scene staticScene;
        private static Scene dynamicScene;

        public static async Task<(Location, Location)> LoadBoth(Location statLocation, Location dynLocation)
        {
            void InnerLoadingScenes(object[] obj)
            {
                GEvent.Detach(GlobalEvents.BothLocationUnloaded, InnerLoadingScenes);
                LoadingScenes(statLocation, dynLocation);
            }

            if (staticScene.isLoaded && statLocation.RootSceneName != staticScene.name ||
                dynamicScene.isLoaded && dynLocation.RootSceneName != dynamicScene.name)
            {
                GEvent.Attach(GlobalEvents.BothLocationUnloaded, InnerLoadingScenes);

                await UnloadScenes(LocationSection.StatLocation, LocationSection.DynLocation);
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

            GEvent.Call(GlobalEvents.BothLocationLoaded, statLocation, dynLocation);
            return (statLocation, dynLocation);
        }

        private static void LoadingScenes(Location statLocation, Location dynLocation)
        {
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

        public static async void DropBoth(Location statLocation, Location dynamicLocation)
        {
            statLocation.Drop();
            dynamicLocation.Drop();
        }

        private static async Task UnloadScenes(Location statLocation, Location dynamicLocation)
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

            GEvent.Call(GlobalEvents.BothLocationUnloaded);
        }
    }
}