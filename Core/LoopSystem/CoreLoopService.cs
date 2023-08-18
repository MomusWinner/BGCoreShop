using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.LoopSystem
{
    public static class CoreLoopService
    {
        public static int LoopsCount => loops.Count;
        
        private static readonly List<CoreLoop> loops = new List<CoreLoop>();

        public static CoreLoop GetLoop(int type)
        {
            return loops[type];
        }

        public static int AddNewLoop()
        {
            var loop = loops.Count;
            loops.Add(new CoreLoop(loop));
            return loop;
        }

        public static void Execute(int type)
        {
            if (loops.Count is 0)
            {
                if (Application.isPlaying)
                {
                    throw new Exception($"Loop {type} is empty.");
                }
                return;
            }

            loops[type].ExecuteAllEvents();
        }

        public static void ClearLoops()
        {
            loops.Clear();
        }
    }
}