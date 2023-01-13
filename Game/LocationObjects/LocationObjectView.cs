using Core.Locations.Model;
using Core.ObjectsSystem;
using Game.Settings;
using UnityEngine;

namespace GameLogic.Views
{
    public abstract class LocationObjectView<TSetting, TObject> : BaseDroppable
        where TSetting : ViewSetting
        where TObject : Component
    {
        public virtual Vector3 Position
        {
            get => Root.transform.position;
            set => Root.transform.position = value;
        }

        public virtual Quaternion Rotation
        {
            get => Root.transform.rotation;
            set => Root.transform.rotation = value;
        }
        
        public TObject Root { get; set; }
        
        protected readonly TSetting setting;
        protected readonly TObject resource;
        
        protected LocationObjectView(TSetting setting)
        {
            resource = Resources.Load<TObject>(setting.RootObjectPath);
            if(!resource)
                Debug.LogError($"<COLOR=YELLOW>{typeof(TObject).Name}</COLOR> is not loaded from {setting.RootObjectPath}");
            this.setting = setting;
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            CreateView(location?.Root.transform);
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
    }
}