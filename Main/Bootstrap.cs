using UnityEngine;

namespace Core.Main
{
    public static class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod]
        private static void InitData()
        {
            GEvent.Attach(GlobalEvents.Start, OnStart);
            GEvent.Attach(GlobalEvents.Restart, OnRestart);
            Application.targetFrameRate = 60;
        }

        private static void OnStart(object _)
        {
            GEvent.Detach(GlobalEvents.Start, OnStart);
            GlobalState.InProcess = true;
            GlobalState.Initialized = false;
           
            
            
        }

        private static void OnRestart(object _)
        {
            
        }

        private static void LoadNonConsumable()
        {
            
        }
        
        private static void LoadConsumable()
        {
            
        }
    }
}
