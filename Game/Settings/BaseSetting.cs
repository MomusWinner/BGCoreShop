using Core.ObjectsSystem;
using Game.UI;
using GameData;
using UnityEngine;

namespace Game.Settings
{
    public abstract class BaseSetting : ScriptableObject
    {
        public abstract IDroppable GetInstance<TContext>(TContext context, IDroppable parent) where TContext : IContext;
    }

}