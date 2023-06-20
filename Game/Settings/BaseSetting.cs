using Core.ObjectsSystem;
using GameData;
using UnityEngine;

namespace Game.Settings
{
    public abstract class BaseSetting : ScriptableObject, ISetting
    {
        public abstract IDroppable GetInstance<TContext>(TContext context) where TContext : IContext;
    }

    public interface ISetting
    {
        IDroppable GetInstance<TContext>(TContext context) where TContext : IContext;
    }
}