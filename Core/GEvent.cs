using System;
using System.Collections.Generic;
using Core.ObjectsSystem;
using UnityEngine;

namespace Core
{
    public static class GEvent
    {
        private static uint staticUniqueCounter;

        private static readonly Dictionary<string, Dictionary<int, Action<object[]>>> actions =
            new Dictionary<string, Dictionary<int, Action<object[]>>>();

        public static string GetUniqueCategory()
        {
            return "#" + staticUniqueCounter++;
        }

        public static void Attach(string category, Action<object[]> method, IDroppable puller = null)
        {
            if (actions.TryGetValue(category, out var actionsList))
            {
                if (actionsList.ContainsKey(method.GetHashCode()))
                {
                    Debug.LogWarning($"Same method attaching {method.Method}");
                    return;
                }
                actionsList.Add(method.GetHashCode(), method);
            }
            else
            {
                actions.Add(category, new Dictionary<int, Action<object[]>> {{method.GetHashCode(), method}});
            }

            if (puller is { })
            {
                puller.Dropped += droppable => Detach(category, method);
            }
        }

        public static void AttachOnce(string category, Action<object[]> method, IDroppable puller = null)
        {
            if (actions.TryGetValue(category, out var actionsMap))
            {
                if (actionsMap.ContainsKey(method.GetHashCode()))
                {
                    return;
                }
            }

            Attach(category, method, puller);
        }

        public static void Detach(string category, Action<object[]> method)
        {
            if (actions.TryGetValue(category, out var actionMap))
            {
                var hash = method.GetHashCode();
                if (actionMap.ContainsKey(hash))
                {
                    actionMap.Remove(hash);
                }
                
                if (actionMap.Count is 0)
                {
                    actions.Remove(category);
                }
            }
        }

        public static void Call(string category, params object[] objects)
        {
            if (!actions.TryGetValue(category, out var actionsList))
            {
                return;
            }

            if (actionsList is {Count: 0})
            {
                return;
            }

            var copy = new Action<object[]>[actionsList.Count];
            actionsList.Values.CopyTo(copy, 0);

            foreach (var action in copy)
            {
                action(objects);
            }
        }
    }
}