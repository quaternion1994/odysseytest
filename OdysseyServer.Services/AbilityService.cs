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

        public async Task<AbilityGetResponse> GetAbilityById(AbilityGetRequest requestObject)
        {
            var abilityDbo = await _unitOfWork.Ability.GetByID(requestObject.AbilityId);
            var ability = _mapper.Map<Ability>(abilityDbo);            
            ability.RowVersion = Helper.ConvertByteArryyToByteString(abilityDbo.RowVersion);
            ability.Stats.RowVersion = Helper.ConvertByteArryyToByteString(abilityDbo.Stats.RowVersion);
            var result = new AbilityGetResponse
            {
                Ability = ability
            };
            return result;
        }

        public async Task<AbilityAddResponse> CreateAbility(AbilityAddRequest requestObject)
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
          
        public async Task<AbilityAllResponse> GetAllAbilities()
        {
            var abilitiesDbo = await _unitOfWork.Ability.Get(includeProperties: "Stats");

            var allAbilityDbo = new AllAbilityDbo
            {
                Abilities = abilitiesDbo
            };
            var ability = _mapper.Map<AllAbility>(allAbilityDbo);
            var result = new AbilityAllResponse
            {
                Ability = ability
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

        public async Task DeleteAbility(long abilityId)
        {
            await _unitOfWork.Ability.Delete(abilityId);
        }
    }
}
