using System;

namespace Core.Main.LoopSystem
{
    public abstract class Loopable 
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
        
        public void LoopOn(int type, Action action, bool callNow = false)
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
        
        public Action GetAction(int type)
        {
            return actions[type];
        }

    }
}