using System;
using System.Collections.Generic;
using Core.ObjectsSystem;
using Game.Characters.Control;
using Game.Characters.Model;
using GameData;
using GameLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Characters.View
{
    public abstract class BaseCharacterView : BaseDroppable, ICharacterView
    {
        public GameObject Root { get; protected set; }
        protected IContext Context { get; }

        protected readonly Object resource;
        protected readonly IDictionary<Type, IReceiver> receivers = new Dictionary<Type, IReceiver>();
        
        protected BaseCharacterView(BaseCharacterSetting setting, IContext context)
        {
            resource = Resources.Load(setting.RootObjectPath);
            Context = context;
        }
        
        protected override void OnAlive()
        {
            base.OnAlive();
            if (resource)
            {
                Root = (GameObject) Object.Instantiate(resource, ChapterContainer.ActiveSection.DynLocation.Root.transform);
                Root.name = $"[{Name}] {resource.name}";
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