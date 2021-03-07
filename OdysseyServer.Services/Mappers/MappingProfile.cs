using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Character, CharacterDbo>();
            CreateMap<CharacterDbo, Character>();
            CreateMap<AllCharacterDbo, AllCharacter>();
            CreateMap<Ability, AbilityDbo>();
            CreateMap<AbilityDbo, Ability>();
            CreateMap<AllAbilityDbo, AllAbility>();
            CreateMap<AbilityStatsDbo, AbilityStats>();
            CreateMap<AbilityStats, AbilityStatsDbo>();
            CreateMap<Group, GroupDbo>();
            CreateMap<GroupDbo, Group>();
        }
    }
}
