using System;
using Core.Main.Locations;
using Core.Main.ObjectsSystem;
using UnityEngine;

namespace Core.Main
{
    public class LocationSection : MonoBehaviour, IDroppable
    {
        public string Name { get; }
        public bool Alive { get; private set; }

        public event Action<IDroppable> Dropped;

        private Location staticLocation;
        private Location dynamicLocation;

        private LocationView staticLocationView;
        private LocationView dynamicLocationView;

        private void Awake()
        {
            SetAlive();
            Shoulder.Instance.InvokeWhen(() => GlobalState.Initialized, OnStart);
        }

        private void OnStart()
        {
            GEvent.Attach(GlobalEvents.DropSection, _ => Drop(), this);

            staticLocationView = new LocationView(staticLocation);
            dynamicLocationView = new LocationView(dynamicLocation);
        }

        private void OnDrop()
        {
            staticLocation?.Drop();
            dynamicLocation?.Drop();
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