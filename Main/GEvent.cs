using System;
using System.Collections.Generic;
using Core.Main.ObjectsSystem;

namespace Core.Main
{
    public static class GEvent
    {
        private static uint staticUniqueCounter;

        private static readonly Dictionary<string, List<Action<object[]>>> actions =
            new Dictionary<string, List<Action<object[]>>>();

        public static string GetUniqueCategory()
        {
            return "#" + staticUniqueCounter++;
        }

        public static void Attach(string category, Action<object[]> method, IDroppable puller = null)
        {
            if (actions.TryGetValue(category, out var actionsList))
            {
                actionsList.Add(method);
            }
            else
            {
                actions.Add(category, new List<Action<object[]>> {method});
            }

            if (puller is { })
            {
                puller.Dropped += droppable => Detach(category, method);
            }
        }

        public static void Detach(string category, Action<object[]> method)
        {
            if (actions.TryGetValue(category, out var actionsList))
            {
                for (var i = actionsList.Count - 1; i >= 0; i--)
                {
                    actionsList.RemoveAt(i);
                }

                if (actionsList.Count is 0)
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


            switch (actionsList.Count)
            {
                case 1:
                {
                    actionsList[0](objects);
                    break;
                }
                case 2:
                {
                    var tmp0 = actionsList[0];
                    var tmp1 = actionsList[1];
                    tmp0(objects);
                    tmp1(objects);
                    break;
                }
                case 3:
                {
                    var tmp0 = actionsList[0];
                    var tmp1 = actionsList[1];
                    var tmp2 = actionsList[2];
                    tmp0(objects);
                    tmp1(objects);
                    tmp2(objects);
                    break;
                }
                default:
                {
                    var copy = new Action<object> [actionsList.Count];
                    actionsList.CopyTo(copy, 0);
                    foreach (var action in copy)
                        action(objects);
                    break;
                }
            }
        }
    }
}