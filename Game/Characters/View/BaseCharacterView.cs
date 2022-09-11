using Core.ObjectsSystem;
using Game.Characters.Model;
using UnityEngine;

namespace Game.Characters.View
{
    public abstract class BaseCharacterView : BaseDroppable, ICharacterView
    {
        public GameObject Root { get; private set; }
        public Transform ParentTransform { get; }

        private Object resource;

        protected BaseCharacterView(string name, BaseCharacterSetting setting, Transform parent = null) : base(name)
        {
            resource = Resources.Load(setting.RootObjectPath);
            ParentTransform = parent;
        }

        public override void SetAlive()
        {
            base.SetAlive();
            if (resource)
            {
                Root = (GameObject) Object.Instantiate(resource, ParentTransform);
                Root.name = Name;
            }
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            Object.DestroyImmediate(Root);
        }
    }
}