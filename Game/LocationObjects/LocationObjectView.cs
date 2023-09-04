using System;
using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.LocationObjects;
using Game.Settings;
using Game.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic.Views
{
    public abstract class LocationObjectView<TSetting, TObject> : BaseDroppable, ILocationObject
        where TSetting : ViewSetting
        where TObject : Component
    {
        public Guid Id { get; }
        public int LoadOrder { get; private set; }
        
        public void SetLoadOrder(int order)
        {
            LoadOrder = order;
        }

        public Transform Transform => Root.transform;

        public TObject Root { get; set; }

        protected readonly TSetting setting;
        protected TObject resource;

        protected LocationObjectView(TSetting setting, IDroppable parent) : base(parent)
        {
            this.setting = setting;
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            var transform = parent switch
            {
                ILocationObject locationObject => locationObject.Transform,
                Location location => location.RootTransform,
                _ => null
            };

            CreateView(transform);
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            if (Root)
                Object.DestroyImmediate(Root.gameObject);
            Root = null;
        }

        protected void CreateView(Transform parent)
        {
            Root = Object.Instantiate(resource, parent);
            Debug.Log($"Created {setting.name} -> [{GetType().Name}] {resource.name}");
            Root.name = $"[{GetType().Name}] {resource.name}";
        }
    }
}