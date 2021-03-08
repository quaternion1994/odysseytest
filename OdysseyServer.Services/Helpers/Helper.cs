using Google.Protobuf;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OdysseyServer.Services.Helpers
{
    public static class Helper
    {
        public static ByteString ConvertByteArryyToByteString(byte[] array)
        {
            Stream stream = new MemoryStream(array);
            var byteString = ByteString.FromStream(stream);
            return byteString;
        }

        public static List<Ability> ConvertToAbility(List<AbilityDbo> abilityDbosList)
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

        public static List<AbilityDbo> ConvertToAbilityDbo(List<Ability> abilityList)
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

        public static List<Group> ConvertToGroup(List<GroupDbo> groupDbosList)
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

        public static List<GroupDbo> ConvertToGroupDbo(List<Group> groupList)
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
    }
}
