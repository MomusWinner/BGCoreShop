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

            if (obj.Length > 1 && obj[0] is LocationSetting statSetting && obj[1] is LocationSetting dynSetting)
            {
                (statLocation, dynLocation) = await LocationFactory.CreateLocation(statSetting, dynSetting);
            }
        }

        private void OnDrop()
        {
            LocationFactory.DropLocation(statLocation, dynLocation);

            statLocation = null;
            dynLocation = null;
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