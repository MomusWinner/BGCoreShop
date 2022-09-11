using System;
using System.Collections.Generic;
using Core.Entities.Loopables;

namespace Core.LoopSystem
{
    public class LoopSession
    {
        public bool Destroyed { get; set; }

        public List<Loopable> Process { get; } = new List<Loopable>();
        public List<Loopable> ForAdd { get; } = new List<Loopable>();
        public List<Loopable> ForRemove { get; } = new List<Loopable>();

        public List<Action> Actions { get; } = new List<Action>();
        public int ActionsCount { get; set; }
        public object SyncRoot { get; } = new object();

    }
}