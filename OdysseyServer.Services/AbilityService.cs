using AutoMapper;
using Google.Protobuf;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System.Threading.Tasks;
using OdysseyServer.Services.Helpers;

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
            var abilityCreatedDbo = await _unitOfWork.Ability.GetByID(abilityDbo.Id);
            var ability = _mapper.Map<Ability>(abilityCreatedDbo);
            var result = new AbilityAddResponse
            {
                Ability = ability
            };
            return result;
        }

        public async Task<AllAbility> GetAllAbilities()
        {
            var abilitiesDbo = await _unitOfWork.Ability.Get(includeProperties: "Stats");
            var a = new AllAbilityDbo
            {
                Abilities = abilitiesDbo
            };
            var ability = _mapper.Map<AllAbility>(a);
            return ability;
        }

        public async Task<AbilityUpdateResponse> UpdateAbility(AbilityUpdateRequest requestObject)
        {
            var abilityDbo = _mapper.Map<AbilityDbo>(requestObject.Ability);            
            abilityDbo.RowVersion = requestObject.Ability.RowVersion.ToByteArray();
            abilityDbo.Stats.RowVersion = requestObject.Ability.Stats.RowVersion.ToByteArray();
            await _unitOfWork.Ability.Update(abilityDbo);
            var abilityDboUpdated = await _unitOfWork.Ability.GetByID(abilityDbo.Id);
            var ability = _mapper.Map<Ability>(abilityDboUpdated);
            var result = new AbilityUpdateResponse
            {
                Ability = ability
            };
            return result;
        }

        public async Task DeleteAbility(AbilityDeleteRequest requestObject)
        {
            await _unitOfWork.Ability.Delete(requestObject.AbilityId);
        }
    }
}
