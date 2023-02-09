using System;
using Core.LoopSystem;
using Core.ObjectsSystem;

namespace Core.Entities.Loopables
{
    public abstract class Loopable : BaseDroppable
    {
        public bool CallActions { get; protected set; }
        public bool CallWhenAdded { get; protected set; }

        private readonly Action[] actions;
        private readonly uint[] orders;

        protected Loopable()
        {
            actions = new Action[CoreLoopService.LoopsCount];
            orders = new uint[CoreLoopService.LoopsCount];
            CallActions = true;
        }

        protected void LoopOn(int type, Action action, bool callNow = false)
        {
            if (actions[type] is null)
            {
                CallWhenAdded = callNow;
                CoreLoopService.GetLoop(type).Add(this);
                actions[type] = action;
            }
            else
            {
                throw new Exception("The loop is already registered");
            }
        }

        public void LoopOff(int type)
        {
            if (actions[type] is { })
            {
                CoreLoopService.GetLoop(type).Remove(this);
                actions[type] = null;
            }
        }
        
        public Action GetAction(int type)
        {
            return actions[type];
        }
        
        public uint GetOrder(int type)
        {
            return orders[type];
        }
        
        public void SetOrder(int loopType, uint order)
        {
            orders[loopType] = order;
        }

        protected override void OnDrop()
        {
            CallActions = false;
            for (var i = 0; i < actions.Length; i++)
            {
                LoopOff(i);
            }
            
            base.OnDrop();
        }
    }
}