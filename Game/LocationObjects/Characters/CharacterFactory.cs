using Core.Locations.Model;
using Game.Characters.Model;
using Game.GameData;
using GameLogic;

namespace Game.Characters
{
    public static class CharacterFactory
    {
        public static ICharacter CreateCharacter(Location parentLocation, CharacterType characterType,
            BaseCharacterSetting setting, IContext context)
        {
            if (setting is PlayerSetting playerSetting &&
                characterType is CharacterType.Player )
            {
                return new Player(parentLocation, "[Player]", playerSetting, context);
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