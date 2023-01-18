using System;
using System.Threading.Tasks;
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

        private static object locker = new object();
        
        public static async Task LoadBothAsync(Location statLocation, Location dynLocation)
        {
            void InnerLoadingScenes(object[] obj)
            {
                GEvent.Detach(GlobalEvents.LocationScenesUnloaded, InnerLoadingScenes);
                LoadingScenes(statLocation, dynLocation);
            }

            if (staticScene.isLoaded && statLocation.Name != staticScene.name ||
                dynamicScene.isLoaded && dynLocation.Name != dynamicScene.name ||
                !staticScene.isLoaded || !dynamicScene.isLoaded)
            {
                GEvent.Attach(GlobalEvents.LocationScenesUnloaded, InnerLoadingScenes);

                await UnloadScenesAsync();
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

        public static void LoadBoth(Location statLocation, Location dynLocation, Action afterSceneLoaded)
        {
            void InnerLoadingScenes(object[] obj)
            {
                GEvent.Detach(GlobalEvents.LocationScenesUnloaded, InnerLoadingScenes);
                Scheduler.InvokeWhen(() => isLoadedDynamicLocation && isLoadedStaticLocation,
                    afterSceneLoaded.Invoke);
                LoadingScenes(statLocation, dynLocation);
            }

            Debug.Log("Start load");
            if (staticScene.isLoaded && statLocation.Name != staticScene.name ||
                dynamicScene.isLoaded && dynLocation.Name != dynamicScene.name ||
                !staticScene.isLoaded || !dynamicScene.isLoaded)
            {
                GEvent.Attach(GlobalEvents.LocationScenesUnloaded, InnerLoadingScenes);

                UnloadScenes();
            }
            else if (!staticScene.isLoaded && !dynamicScene.isLoaded)
            {
                InnerLoadingScenes(null);
            }
            else
            {
                afterSceneLoaded.Invoke();
            }
        }


        private static void LoadingScenes(Location statLocation, Location dynLocation)
        {
            var haveStatic = statLocation is { };
            var haveDynamic = dynLocation is { };

            if (!haveStatic)
            {
                isLoadedStaticLocation = true;
            }
            else
            {
                var loadingStatic = SceneManager.LoadSceneAsync(statLocation.Name, LoadSceneMode.Additive);

                loadingStatic.completed += _ =>
                {
                    lock (locker)
                    {
                        staticScene = SceneManager.GetSceneByName(statLocation.Name);
                        isLoadedStaticLocation = true;
                    }
                };
            }

            if (!haveDynamic)
            {
                isLoadedDynamicLocation = true;
            }
            else
            {
                var loadingDynamic = SceneManager.LoadSceneAsync(dynLocation.Name, LoadSceneMode.Additive);

                loadingDynamic.completed += _ =>
                {
                    lock (locker)
                    {
                        dynamicScene = SceneManager.GetSceneByName(dynLocation.Name);
                        isLoadedDynamicLocation = true;
                    }
                };
            }

            if (!haveDynamic && !haveStatic)
            {
                Debug.LogError("Scene load failed");
            }
        }

        private static async Task UnloadScenesAsync()
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

        private static void UnloadScenes()
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

            Scheduler.InvokeWhen(() => !isLoadedDynamicLocation && !isLoadedStaticLocation,
                () => { GEvent.Call(GlobalEvents.LocationScenesUnloaded); });
        }
    }
}