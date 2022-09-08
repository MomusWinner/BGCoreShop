using System;
using Core.Main.Locations;
using Core.Main.ObjectsSystem;
using Submodules.BGLogic.Main.Locations;
using UnityEngine;

namespace Core.Main
{
    public class LocationSection : MonoBehaviour, IDroppable
    {
        public string Name { get; }
        public bool Alive { get; private set; }

        public event Action<IDroppable> Dropped;

        public static Location StatLocation { get; private set; }
        public static Location DynLocation { get; private set; }

        private void Awake()
        {
            SetAlive();

            GEvent.Attach(GlobalEvents.Start, OnStart);
        }

        private async void OnStart(params object[] obj)
        {
            GEvent.Attach(GlobalEvents.DropSection, _ => Drop(), this);
            GEvent.Attach(GlobalEvents.Restart, OnRestart);

            if (obj.Length > 1)
            {
                if(obj[0] is LocationSetting statSetting && obj[1] is LocationSetting dynSetting)
                { 
                    var sLocation = new Location(statSetting);
                    var dLocation = new Location(dynSetting);
                    (StatLocation, DynLocation) = await LocationLoader.LoadBoth(sLocation, dLocation);
                }
            }
        }

        private void OnDrop()
        {
            LocationLoader.DropBoth(StatLocation, DynLocation);
        }
        
        private void OnRestart(params object[] objs)
        {
            if (objs.Length > 0 && objs[0] is Location {Alive: true} location)
            {
                location.Drop();
                location.Refresh();
                GEvent.Call(GlobalEvents.LocationLoaded, location);
            }
        }

        public void Drop()
        {
            if (!Alive)
            {
                return;
            }

            Alive = false;
            OnDrop();
        }

        public void SetAlive()
        {
            Alive = true;
        }
    }
}