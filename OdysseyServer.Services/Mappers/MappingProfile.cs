using AutoMapper;
using Google.Protobuf;
using Google.Protobuf.Collections;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Converters;
using System.Collections.Generic;

namespace OdysseyServer.Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>)).ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
            CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));

            CreateMap<CharacterDbo, Character>()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => ByteString.CopyFrom(source.RowVersion)))
                .ForMember(x => x.Power, opt => opt.MapFrom(source => (source.Offence + source.Defence + source.Health / 10) / 3))
            .ReverseMap()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => source.RowVersion.ToByteArray()))
                .ForMember(x => x.Power, opt => opt.MapFrom(source => (source.Offence + source.Defence + source.Health / 10) / 3));

            CreateMap<AllCharacterDbo, AllCharacter>();

            CreateMap<AbilityDbo, Ability>()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => ByteString.CopyFrom(source.RowVersion)))
            .ReverseMap()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => source.RowVersion.ToByteArray()));

            CreateMap<AllAbilityDbo, AllAbility>();

            CreateMap<AbilityStatsDbo, AbilityStats>()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source=> ByteString.CopyFrom(source.RowVersion)))
                .ForMember(x => x.Attack, opt => opt.MapFrom(source => source.Attack))
                .ForMember(x => x.Defence, opt => opt.MapFrom(source => source.Defence))
            .ReverseMap()
                .ForMember(x => x.Attack, opt => opt.MapFrom(source => source.Attack))
                .ForMember(x => x.Defence, opt => opt.MapFrom(source => source.Defence))
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => source.RowVersion.ToByteArray()));

            CreateMap<GroupDbo, Group>()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => ByteString.CopyFrom(source.RowVersion)))
            .ReverseMap()
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(source => source.RowVersion.ToByteArray()));

            CreateMap<AllGroupDbo, AllGroup>();
        }
    }
}
