using System;
using UnityEngine;

namespace Game.LocationObjects
{
    public interface ILocationObject
    {
        public Guid Id { get; }
        public Transform Transform { get; }
    }
}