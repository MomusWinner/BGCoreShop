using System;
using UnityEngine;

namespace Game.LocationObjects
{
    public interface ILocationObject
    {
        public Guid Id { get; }
        public int LoadOrder { get; }
        public void SetLoadOrder(int order);
        public Transform Transform { get; }
    }
}