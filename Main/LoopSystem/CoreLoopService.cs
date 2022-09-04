using System;
using System.Collections.Generic;

namespace Core.Main.LoopSystem
{
    public class CoreLoopService
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
                throw new Exception($"Loop {type} is empty.");
            }

            loops[type].ExecuteAllEvents();
        }
    }
}