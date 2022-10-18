using Core.Locations.Model;
using Game.Characters.Model;
using Game.GameData;
using GameLogic;
//using GameLogic.Npcs;

namespace Game.Characters
{
    public static class CharacterFactory
    {
        public static ICharacter CreateCharacter(Location parentLocation, CharacterType characterType,
            BaseCharacterSetting setting, IContext context)
        {
            if (setting is PlayerSetting playerSetting && characterType is CharacterType.Player )
            {
                return new Player(parentLocation, "[Player]", playerSetting, context);
            }
            // if (setting is NpcSetting npcSetting && characterType is CharacterType.Npc )
            // {
            //     return new Npc(parentLocation, "[NPC]", npcSetting, context);
            // }

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