using AutoMapper;
using Google.Protobuf;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System.Threading.Tasks;
using OdysseyServer.Services.Helpers;
using OdysseyServer.Services.Converters;

namespace OdysseyServer.Services
{
    public class AbilityService : IAbilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AbilityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AbilityGetResponse> GetAbilityByIdAsync(long abilityId)
        {
            var abilityDbo = await _unitOfWork.Ability.GetByID(abilityId);
            var ability = _mapper.Map<Ability>(abilityDbo);            
            ability.Stats.RowVersion = ByteString.CopyFrom(abilityDbo.Stats.RowVersion);
            var result = new AbilityGetResponse
            {
                Ability = ability
            };
            return result;
        }

        public async Task<AbilityAddResponse> CreateAbilityAsync(AbilityAddRequest requestObject)
        {
            var abilityDbo = _mapper.Map<AbilityDbo>(requestObject.Ability);
            await _unitOfWork.Ability.Insert(abilityDbo);
            var ability = _mapper.Map<Ability>(abilityDbo);
            var result = new AbilityAddResponse
            {
                Ability = ability
            };
            return result;
        }
          
        public async Task<AbilityAllResponse> GetAllAbilitiesAsync()
        {
            var result = new AbilityAllResponse
            {
                Ability = _mapper.Map<AllAbility>(new AllAbilityDbo
                {
                    Abilities = await _unitOfWork.Ability.Get(includeProperties: "Stats")
                })
            };
            return result;
        }

        public async Task<AbilityUpdateResponse> UpdateAbility(AbilityUpdateRequest requestObject)
        {
            var abilityFromDb = await _unitOfWork.Ability.GetByID(requestObject.Ability.Id);
            Converter.UpdateDboByAbility(requestObject.Ability, abilityFromDb);
            await _unitOfWork.Ability.SaveChangesAsync();
            var ability = _mapper.Map<Ability>(abilityFromDb);
            var result = new AbilityUpdateResponse
            {
                Ability = ability
            };
            return result;
        }

        public async Task DeleteAbilityAsync(long abilityId)
        {
            await _unitOfWork.Ability.Delete(abilityId);
        }
    }
}
