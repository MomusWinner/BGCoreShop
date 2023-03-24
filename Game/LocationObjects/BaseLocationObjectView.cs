using System;
using Contexts;
using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.LocationObjects;
using Game.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic.Views
{
    public abstract class BaseLocationObjectView<TSetting, TObject> : BaseDroppable, ILocationObject
        where TSetting : ViewSetting
        where TObject : Component
    {
        public Transform Transform => Root.transform;

        protected virtual Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        protected virtual Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }
        
        public TObject Root { get; set; }
        
        protected readonly TSetting setting;
        protected readonly TObject resource;
        protected readonly IContext context;
        protected BaseLocationObjectView(TSetting setting, IContext context)
        {
            this.context = context;
            resource = Resources.Load<TObject>(setting.rootObjectPath);
            if(!resource)
                Debug.LogError($"<COLOR=YELLOW>{typeof(TObject).Name}</COLOR> is not loaded from {setting.rootObjectPath}");
            this.setting = setting;
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            var transform = parent switch
            {
                ILocationObject locationObject => locationObject.Transform,
                Location location => location.Root.transform,
                _ => null
            };
            CreateView(transform);
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            if(Root)
                Object.DestroyImmediate(Root.gameObject);
            Root = null;
        }
        
        protected void CreateView(Transform parent)
        {
            Root = Object.Instantiate(resource, parent);
            Root.name = $"[{GetType().Name}] {resource.name}";
        }

        public Guid Id { get; }
    }
}