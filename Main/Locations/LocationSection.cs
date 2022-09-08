using System;
using Core.Main.Locations;
using Core.Main.ObjectsSystem;
using Submodules.BGLogic.Main.Locations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Main
{
    public class LocationSection : MonoBehaviour, IDroppable
    {
        public string Name { get; }
        public bool Alive { get; private set; }

        public event Action<IDroppable> Dropped;

        private Location statLocation;
        private Location dynLocation;

        private void Awake()
        {
            SetAlive();

            GEvent.Attach(GlobalEvents.Start, OnStart);
        }

        private async void OnStart(params object[] obj)
        {
            GEvent.Attach(GlobalEvents.DropSection, _ => Drop(), this);
            GEvent.Attach(GlobalEvents.Restart, OnRestart);

            if (obj.Length > 1 && obj[0] is LocationSetting statSetting && obj[1] is LocationSetting dynSetting)
            {
                (statLocation, dynLocation) = await LocationFactory.CreateLocation(statSetting, dynSetting);
            }
        }

        private async void OnDrop()
        {
            await LocationFactory.DropLocation(statLocation, dynLocation);

            statLocation = null;
            dynLocation = null;
        }
        
        private void OnRestart(params object[] objs)
        {
            GEvent.Detach(GlobalEvents.Restart, OnRestart);
            GEvent.Attach(GlobalEvents.LocationUnloaded, OnStart);
            GEvent.Call(GlobalEvents.DropSection);
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