using Core;
using Game.Characters.Model;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(menuName = "Game/Settings/PlayerSetting", fileName = "PlayerSetting", order = 1)]
    public class PlayerSetting : BaseCharacterSetting
    {
        [Header("Controller settings:")] 
        public Object playerControllerView;
        public string playerControllerPrefabPath;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            if (playerControllerView)
            {
#if UNITY_EDITOR
                playerControllerPrefabPath = Utilities.GetValidPathToResource(playerControllerView);
#endif
            }
        }
    }
}