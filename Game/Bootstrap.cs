using System;
using Core.Chapters;
using GameLogic;
using UnityEngine;

namespace Game
{
    public partial class Bootstrap
    {
        private static ContainerData data;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Initiate();
            MonoLoop.Initiate();
            Container.Initiate(data);
        }

        static partial void Initiate();
        
        partial void Update() => MonoLoop.Update();
        partial void FixedUpdate() => MonoLoop.FixedUpdate();
        partial void LateUpdate() => MonoLoop.LateUpdate();

    }

    [Serializable]
    public partial class ContainerData
    {
        public Chapter[] chapters;
    }
}