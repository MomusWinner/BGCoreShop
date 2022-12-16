using Core.ObjectsSystem;
using Game.Characters.Control;
using Game.Characters.Model;
using GameData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Characters.View
{
    public abstract class BaseCharacterView : BaseDroppable, ICharacterView
    {
        public GameObject Root { get; private set; }
        public Transform ParentTransform { get; }
        public IContext Context { get; }

        private readonly Object resource;

        protected BaseCharacterView(string name, BaseCharacterSetting setting, IContext context, Transform parent = null) : base(name)
        {
            ParentTransform = parent;
            resource = Resources.Load(setting.RootObjectPath);
            Context = context;
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

        public abstract void Pull(ICommand command);
        public abstract void Action(ICommand command);
        public abstract void ExecuteCommands();
    }
}