using System;
using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.LocationObjects;
using Game.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace GameLogic.Views
{
    public abstract class LocationObjectView<TSetting, TObject> : BaseDroppable, ILocationObject
        where TSetting : ViewSetting
        where TObject : Component
    {
        public Guid Id { get; }
        public Transform Transform => Root.transform;

        public TObject Root { get; set; }
        
        protected readonly TSetting setting;
        protected TObject resource;
        private bool isResourceLoaded;
        private readonly AsyncOperationHandle<TObject> opHandler;

        protected LocationObjectView(TSetting setting)
        {
            isResourceLoaded = false;
            opHandler =  Addressables.LoadAssetAsync<TObject>(setting.rootObjectPath);
            opHandler.Completed += handle =>
            {
                resource = handle.Result;
                if (!resource)
                {
                    Debug.LogError($"<COLOR=YELLOW>{typeof(TObject).Name}</COLOR> is not loaded from {setting.rootObjectPath}");
                    return;
                }

                Root = Object.Instantiate(resource);
                Root.name = $"[{GetType().Name}] {resource.name}";
                isResourceLoaded = true;  
            };
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
            Addressables.Release(opHandler);
            Root = null;
        }
        
        protected void CreateView(Transform parent)
        {
            Root.transform.SetParent(parent);
        }
    }
}