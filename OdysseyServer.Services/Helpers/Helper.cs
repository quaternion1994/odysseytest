using Google.Protobuf;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OdysseyServer.Services.Helpers
{
    public static class Helper
    {
        public static List<Ability> ConvertToAbility(this IEnumerable<AbilityDbo> abilityDbosList)
        {
            var resultList = new List<Ability>();
            foreach (var elem in abilityDbosList)
            {
                var convertedAbility = new Ability();
                convertedAbility = Converter.AbilityDboToAbility(convertedAbility, elem);
                resultList.Add(convertedAbility);
            }
            return resultList;
        }

        public static List<AbilityDbo> ConvertToAbilityDbo(IEnumerable<Ability> abilityList)
        {
            var resultList = new List<AbilityDbo>();
            foreach (var elem in abilityList)
            {
                var convertedAbility = new AbilityDbo();
                convertedAbility = Converter.AbilityToAbilityDbo(elem, convertedAbility);
                resultList.Add(convertedAbility);
            }
            return resultList;
        }

        public static List<Group> ConvertToGroup(IEnumerable<GroupDbo> groupDbosList)
        {
            var resultList = new List<Group>();
            foreach (var elem in groupDbosList)
            {
                var convertedGroup = new Group();
                convertedGroup = Converter.GroupDboToGroup(convertedGroup, elem);
                resultList.Add(convertedGroup);
            }
            return resultList;
        }

        public static List<GroupDbo> ConvertToGroupDbo(IEnumerable<Group> groupList)
        {
            var resultList = new List<GroupDbo>();
            foreach (var elem in groupList)
            {
                var convertedGroupDbo = new GroupDbo();
                convertedGroupDbo = Converter.GroupToGroupDbo(elem, convertedGroupDbo);
                resultList.Add(convertedGroupDbo);
            }
            return resultList;
        }

        public static List<AbilityDbo> GetMaxLevelAbilities(IEnumerable<AbilityDbo> abilities)
        {
            var groupedAbility = abilities.GroupBy(x => x.AbilityType);
            var currentAbility = new List<AbilityDbo>();
            foreach (var elem in groupedAbility)
            {
                var maxLevelAbility = elem.OrderByDescending(x => x.Level).First();
                currentAbility.Add(maxLevelAbility);
            }
            return currentAbility;
        }
    }
}
