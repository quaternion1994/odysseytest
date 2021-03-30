using Google.Protobuf;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdysseyServer.Services.Converters
{
    public static class Converter
    {
        public static AbilityDbo UpdateDboByAbility(this Ability ability, AbilityDbo abilityDbo)
        {
            abilityDbo.Id = ability.Id;
            abilityDbo.Level = ability.Level;            
            abilityDbo.RequiredLevel = ability.RequiredLevel;
            abilityDbo.Name = ability.Name;
            abilityDbo.RowVersion = ability.RowVersion.ToByteArray();
            abilityDbo.AbilityType = ability.AbilityType;
            if (ability.Stats != null)
            {
                abilityDbo.Stats.RowVersion = ability.Stats.RowVersion.ToByteArray();
                abilityDbo.Stats.Attack = ability.Stats != null ? ability.Stats.Attack : abilityDbo.Stats.Attack;
                abilityDbo.Stats.Defence = ability.Stats != null ? ability.Stats.Defence : abilityDbo.Stats.Defence;
            }

            return abilityDbo;
        }

        public static CharacterDbo UpdateDboByCharacter(this Character character, CharacterDbo characterDbo)
        {
            characterDbo.Level = character.Level;
            characterDbo.GearTier = character.GearTier;
            characterDbo.Name = character.Name;
            characterDbo.Power = character.Power;
            characterDbo.RowVersion = character.RowVersion.ToByteArray();
            characterDbo.Xp = character.Xp;
            characterDbo.Offence = character.Offence;
            characterDbo.Defence = character.Defence;
            characterDbo.Health = character.Health;
            characterDbo.XpToNextLevel = character.XpToNextLevel;
            return characterDbo;
        }

        public static CharacterDbo CharacterToCharacterDbo(this Character character, CharacterDbo characterDbo, List<AbilityDbo> abilities, List<GroupDbo> groups)
        {
            characterDbo.Id = character.Id;
            characterDbo.GearTier = character.GearTier;
            characterDbo.Level = character.Level;
            characterDbo.Name = character.Name;
            characterDbo.Power = (character.Offence + character.Defence + character.Health / 10) / 3;
            characterDbo.Xp = character.Xp;
            characterDbo.GearTier = character.GearTier;
            characterDbo.Abilities = abilities;
            characterDbo.Groups = groups;
            characterDbo.RowVersion = character.RowVersion.ToByteArray();
            characterDbo.Offence = character.Offence;
            characterDbo.Defence = character.Defence;
            characterDbo.Health = character.Health;
            characterDbo.XpToNextLevel = character.XpToNextLevel;
            return characterDbo;
        }

        public static Ability AbilityDboToAbility(this Ability ability, AbilityDbo abilityDbo)
        {
            ability.Id = abilityDbo.Id;
            ability.Stats = new AbilityStats();
            ability.Level = abilityDbo.Level;
            ability.Stats.Attack = abilityDbo.Stats.Attack;
            ability.Stats.Defence = abilityDbo.Stats.Defence;
            ability.RequiredLevel = abilityDbo.RequiredLevel;
            ability.Name = abilityDbo.Name;
            ability.AbilityType = abilityDbo.AbilityType;
            ability.RowVersion = ByteString.CopyFrom(abilityDbo.RowVersion);
            ability.Stats.RowVersion = abilityDbo.Stats != null ? ByteString.CopyFrom(abilityDbo.Stats.RowVersion) : ability.Stats.RowVersion;
            return ability;
        }

        public static AbilityDbo AbilityToAbilityDbo(this Ability ability, AbilityDbo abilityDbo)
        {
            abilityDbo.Id = ability.Id;
            abilityDbo.Stats = new AbilityStatsDbo();
            abilityDbo.Level = ability.Level;
            abilityDbo.Stats.Attack = ability.Stats != null ? ability.Stats.Attack : abilityDbo.Stats.Attack;
            abilityDbo.Stats.Defence = ability.Stats != null ? ability.Stats.Defence : abilityDbo.Stats.Defence;
            abilityDbo.RequiredLevel = ability.RequiredLevel;
            abilityDbo.Name = ability.Name;
            abilityDbo.AbilityType = ability.AbilityType;
            abilityDbo.RowVersion = ability.RowVersion != null ? ability.RowVersion.ToByteArray() : abilityDbo.RowVersion;
            abilityDbo.Stats.RowVersion = ability.Stats != null ? ability.Stats.RowVersion.ToByteArray() : abilityDbo.Stats.RowVersion;
            return abilityDbo;
        }

        public static Group GroupDboToGroup(this Group group, GroupDbo groupDbo)
        {
            group.Id = groupDbo.Id;
            group.IconName = groupDbo.IconName;
            group.Name = groupDbo.Name;
            group.RowVersion = ByteString.CopyFrom(groupDbo.RowVersion);
            return group;
        }

        public static GroupDbo GroupToGroupDbo(this Group group, GroupDbo groupDbo)
        {
            groupDbo.Id = group.Id;
            groupDbo.IconName = group.IconName;
            groupDbo.Name = group.Name;
            groupDbo.RowVersion = group.RowVersion.ToByteArray();
            return groupDbo;
        }

        public static Character CharacterDboToCharacter(this Character character, CharacterDbo characterDbo, List<Ability> abilities, List<Group> groups)
        {
            character.Id = characterDbo.Id;
            character.GearTier = characterDbo.GearTier;
            character.Level = characterDbo.Level;
            character.Name = characterDbo.Name;
            character.Power = (characterDbo.Offence + characterDbo.Defence + characterDbo.Health/10) / 3;
            character.Xp = characterDbo.Xp;
            character.GearTier = characterDbo.GearTier;
            character.Ability.AddRange(abilities);
            character.Group.AddRange(groups);
            character.RowVersion = ByteString.CopyFrom(characterDbo.RowVersion);
            character.Offence = characterDbo.Offence;
            character.Defence = characterDbo.Defence;
            character.Health = characterDbo.Health;
            character.XpToNextLevel = characterDbo.XpToNextLevel;
            return character;
        }
    }
}
