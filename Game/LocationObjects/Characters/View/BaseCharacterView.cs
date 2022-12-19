using System;
using System.Collections.Generic;
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
        public GameObject Root { get; protected set; }
        protected Transform ParentTransform { get; }
        protected IContext Context { get; }

        protected readonly Object resource;
        protected readonly IDictionary<Type, IReceiver> receivers = new Dictionary<Type, IReceiver>();
        
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

            foreach (var receiver in receivers.Values)
                receiver.SetAlive();
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            foreach (var receiver in receivers.Values)
                receiver.Drop();
            Object.DestroyImmediate(Root);
        }

        public virtual void Pull(ICommand command)
        {
            var type = command.GetType();
            if(receivers.TryGetValue(type, out var receiver))
                receiver.Pull(command);
        }

        public virtual void Action(ICommand command)
        {
            var type = command.GetType();
            if(receivers.TryGetValue(type, out var receiver))
                receiver.Action(command);
        }

        public virtual void ExecuteCommands()
        {
            foreach (var receiver in receivers.Values)
                receiver.ExecuteCommands();
        }
    }
}