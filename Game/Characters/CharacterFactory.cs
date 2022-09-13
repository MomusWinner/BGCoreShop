using Game.Characters.Model;
using GameLogic;
using UnityEngine;

namespace Game.Characters
{
    public static class CharacterFactory
    {
        public static ICharacter CreateCharacter(CharacterType characterType, BaseCharacterSetting setting, Transform parent = null)
        {
            if (setting is PlayerSetting playerSetting &&
                characterType is CharacterType.Player)
            {
                return new Player("[Player]", playerSetting, parent);
            }

            return null;
        }
    }

    public enum CharacterType
    {
        None = 0,
        Player = 1,
        Npc = 2,
    }
}