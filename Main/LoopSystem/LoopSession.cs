using System.Collections.Generic;

namespace Core.Main.LoopSystem
{
    public class LoopSession
    {
        public bool Destroyed { get; set; }

        public List<Loopable> Process { get; } = new List<Loopable>();
        public List<Loopable> ForAdd { get; } = new List<Loopable>();
        public List<Loopable> ForRemove { get; } = new List<Loopable>();
    }
}