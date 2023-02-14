using System;
using System.Collections.Generic;
using Game.Characters.Control;
using Game.Settings;
using GameData;
using GameLogic.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Characters.View
{
    public abstract class ReceiverView<TSetting, TObject> : BaseLocationObjectView<TSetting, TObject>, IReceiver
        where TSetting : BaseLocationObjectSetting
        where TObject : Component
    {
        public TObject Root { get; protected set; }

        protected readonly IDictionary<Type, IReceiver> receivers = new Dictionary<Type, IReceiver>();
        
        protected ReceiverView(TSetting setting, IContext context) : base(setting)
        {
        }
        
        protected override void OnAlive()
        {
            base.OnAlive();
            if (resource)
            {
                Root = (TObject) Object.Instantiate(resource);
                Root.name = $"[{Name}] {resource.name}";
            }

            foreach (var receiver in receivers.Values)
                receiver.SetAlive(location);
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