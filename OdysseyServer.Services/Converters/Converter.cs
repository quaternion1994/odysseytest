using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Services.Converters
{
    public static class Converter
    {
        public static AbilityDbo UpdateDboByAbility(Ability ability, AbilityDbo abilityDbo)
        {
            abilityDbo.Level = ability.Level;
            abilityDbo.Stats.Attack = ability.Stats.Attack;
            abilityDbo.Stats.Defence = ability.Stats.Defence;
            abilityDbo.RequiredLevel = ability.RequiredLevel;
            abilityDbo.Name = ability.Name;
            abilityDbo.RowVersion = ability.RowVersion.ToByteArray();
            abilityDbo.Stats.RowVersion = ability.Stats.RowVersion.ToByteArray();
            return abilityDbo;
        }

        public static CharacterDbo CharacterToCharacterDbo(Character character, CharacterDbo characterDbo, List<AbilityDbo> abilities, List<GroupDbo> groups)
        {

            characterDbo.GearTier = character.GearTier;
            characterDbo.Level = character.Level;
            characterDbo.Name = character.Name;
            characterDbo.Power = character.Power;
            characterDbo.Xp = character.Xp;
            characterDbo.GearTier = character.GearTier;
            characterDbo.Abilities = abilities;
            characterDbo.Groups = groups;
            characterDbo.RowVersion = character.RowVersion.ToByteArray();
            return characterDbo;
        }

        public static Ability AbilityDboToAbility(Ability ability, AbilityDbo abilityDbo)
        {
            ability.Stats = new AbilityStats();
            ability.Level = abilityDbo.Level;
            ability.Stats.Attack = abilityDbo.Stats.Attack;
            ability.Stats.Defence = abilityDbo.Stats.Defence;
            ability.RequiredLevel = abilityDbo.RequiredLevel;
            ability.Name = abilityDbo.Name;
            ability.RowVersion = Helper.ConvertByteArryyToByteString(abilityDbo.RowVersion);
            ability.Stats.RowVersion = Helper.ConvertByteArryyToByteString(abilityDbo.Stats.RowVersion);
            return ability;
        }

        public static Group GroupDboToGroup(Group group, GroupDbo groupDbo)
        {
            group.IconName = groupDbo.IconName;
            group.Name = groupDbo.Name;
            group.RowVersion = Helper.ConvertByteArryyToByteString(groupDbo.RowVersion);
            return group;
        }

        public static Character CharacterDboToCharacter(Character character, CharacterDbo characterDbo, List<Ability> abilities, List<Group> groups)
        {

            character.GearTier = characterDbo.GearTier;
            character.Level = characterDbo.Level;
            character.Name = characterDbo.Name;
            character.Power = characterDbo.Power;
            character.Xp = characterDbo.Xp;
            character.GearTier = characterDbo.GearTier;
            character.Ability.AddRange(abilities);
            character.Group.AddRange(groups);
            character.RowVersion = Helper.ConvertByteArryyToByteString(characterDbo.RowVersion);
            return character;
        }
    }
}
