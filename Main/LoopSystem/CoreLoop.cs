using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Core.Main.LoopSystem
{
    public class CoreLoop
    {
        private readonly int loopType;

        private readonly LoopSession session;
        
        public CoreLoop(int type)
        {
            loopType = type;
            session = new LoopSession();
        }

        public void ExecuteAllEvents()
        {
            
        }

        public void Add(Loopable loopable)
        {
            session.ForAdd.Add(loopable);
        }
        
        private void InnerCall(List<Loopable> loopables, LoopSession session)
        {
            for(var i = 0; i < loopables.Count; i++)
            {
                if (session.Destroyed)
                {
                    return;
                }

                var current = loopables[i];
                if (current.CallActions)
                {
                    current.GetAction(loopType)?.Invoke();
                }
            }
        }
        
        
    }
}