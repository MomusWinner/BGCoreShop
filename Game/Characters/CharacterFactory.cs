using Game.Characters.Model;
using GameLogic;
using UnityEngine;

namespace Submodules.BGLogic.Game.Characters
{
    public static class CharacterFactory
    {
        public static ICharacter CreateCharacter(CharacterType characterType, BaseCharacterSetting setting, Transform parent = null)
        {
            if (characterType is CharacterType.Player)
            {
                return new Player("[Player]", setting, parent);
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