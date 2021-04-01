using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System.Threading.Tasks;
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
            AbilityDbo abilityDbo = await _unitOfWork.Ability.GetByIdAsync(abilityId);       
            return new AbilityGetResponse
            {
                Ability = _mapper.Map<Ability>(abilityDbo)
            };
        }

        public async Task<AbilityAddResponse> CreateAbilityAsync(AbilityAddRequest requestObject)
        {
            AbilityDbo abilityDbo = _mapper.Map<AbilityDbo>(requestObject.Ability);
            await _unitOfWork.Ability.Insert(abilityDbo);
            AbilityAddResponse result = new AbilityAddResponse
            {
                Ability = requestObject.Ability
            };
            result.Ability.Id = abilityDbo.Id;
            return result;
        }
          
        public async Task<AbilityAllResponse> GetAllAbilitiesAsync()
        {
            AbilityAllResponse response = new AbilityAllResponse
            {
                Ability = new AllAbility()
            };
            _mapper.Map(await _unitOfWork.Ability.Get(includeProperties: "Stats"), response.Ability.Abilities);
            
            return response;
        }

        public async Task<AbilityUpdateResponse> UpdateAbility(AbilityUpdateRequest requestObject)
        {
            AbilityDbo abilityDbo = await _unitOfWork.Ability.GetByIdAsync(requestObject.Ability.Id);

            _mapper.Map(requestObject.Ability, abilityDbo);

            await _unitOfWork.SaveChangesAsync();
            return new AbilityUpdateResponse
            {
                Ability = requestObject.Ability
            };
        }

        public async Task DeleteAbilityAsync(long abilityId)
        {
            await _unitOfWork.Ability.Delete(abilityId);
        }
    }
}
