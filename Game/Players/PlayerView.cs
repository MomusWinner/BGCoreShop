using Game.Characters.View;
using Game.GameData;
using UnityEngine;

namespace GameLogic
{
    public class PlayerView : BaseCharacterView
    {
        public PlayerView(string name, PlayerSetting setting, IContext context, Transform parent = null) : base(name, setting, context, parent)
        {
        }
    }
}