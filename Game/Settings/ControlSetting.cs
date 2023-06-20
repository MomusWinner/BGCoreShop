using Core.Entities.Loopables;
using GameData;

namespace Game.Settings
{
    public abstract class ControlSetting<TControl> : ViewSetting where TControl : ControlLoopable
    {
        public abstract TControl GetControlLoopable<TContext>(TContext context) where TContext : IContext;
    }
}