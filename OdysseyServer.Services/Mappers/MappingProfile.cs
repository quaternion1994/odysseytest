using AutoMapper;
using Google.Protobuf;
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
            CreateMap<Character, CharacterDbo>()
                .ForMember(x => x.RowVersion, y => y.Ignore());
            CreateMap<CharacterDbo, Character>();
            CreateMap<AllCharacterDbo, AllCharacter>();

            CreateMap<Ability, AbilityDbo>()
                .ForMember(x => x.RowVersion, y => y.Ignore());
            CreateMap<AbilityDbo, Ability>()
                .ForMember(x => x.RowVersion, y => y.Ignore());
            CreateMap<AllAbilityDbo, AllAbility>();

            CreateMap<AbilityStatsDbo, AbilityStats>()
                .ForMember(x => x.RowVersion, y => y.MapFrom(source=> ByteString.CopyFrom(source.RowVersion)))
            .ReverseMap()
                .ForMember(x => x.RowVersion, y => y.MapFrom(source => source.RowVersion.ToByteArray()));

            CreateMap<Group, GroupDbo>()
                .ForMember(x => x.RowVersion, y => y.MapFrom(source => source.RowVersion.ToByteArray())); ;
            CreateMap<GroupDbo, Group>();
            CreateMap<AllGroupDbo, AllGroup>();
        }
    }
}
