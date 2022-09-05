using System;
using System.Collections.Generic;
using Core.Main.ObjectsSystem;

namespace Core.Main
{
    public static class GEvent
    {
        private static uint staticUniqueCounter;

        private static readonly Dictionary<string, List<Action<object>>> actions =
            new Dictionary<string, List<Action<object>>>();

        public static string GetUniqueCategory()
        {
            return "#" + staticUniqueCounter++;
        }

        public static void Attach(string category, Action<object> method, IDroppable puller = null)
        {
            if (actions.TryGetValue(category, out var actionsList))
            {
                actionsList.Add(method);
            }
            else
            {
                actions.Add(category, new List<Action<object>> {method});
            }

            if (puller is { })
            {
                puller.Dropped += droppable => Detach(category, method);
            }
        }

        public static void Detach(string category, Action<object> method)
        {
            if (actions.TryGetValue(category, out var actionsList))
            {
                for(var i = actionsList.Count - 1; i >= 0; i--)
                {
                    actionsList.RemoveAt(i);
                }

                if (actionsList.Count is 0)
                {
                    actions.Remove(category);
                }
            }
        }

        public static void Call(string category, object obj = null)
        {
            if (actions.TryGetValue(category, out var actionsList))
            {
                if (actionsList.Count is 0)
                {
                    actions.Remove(category);
                    return;
                }

                foreach (var action in actionsList)
                {
                    action(obj);
                }
            }
        }
    }
}