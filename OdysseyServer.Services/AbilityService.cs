using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Ability> GetAbilityById(long id)
        {
            var abilityDbo = await _unitOfWork.Ability.GetByID(id);
            var ability = _mapper.Map<Ability>(abilityDbo);
            return ability;
        }

        public async Task CreateAbility(Ability ability)
        {
            var abilityDbo = _mapper.Map<AbilityDbo>(ability);
            await _unitOfWork.Ability.Insert(abilityDbo);
        }

        public async Task<AllAbility> GetAllAbilities()
        {
            var abilitiesDbo = await _unitOfWork.Ability.Get(includeProperties: "Stats");
            var a = new AllAbilityDbo
            {
                Abilities = abilitiesDbo
            };
            var character = _mapper.Map<AllAbility>(a);
            return character;
        }

        public async Task<Ability> UpdateAbility(Ability ability)
        {
            var abilityDbo = _mapper.Map<AbilityDbo>(ability);
            await _unitOfWork.Ability.Update(abilityDbo);
            var characterDboUpdated = await _unitOfWork.Ability.GetByID(abilityDbo.Id);
            var result = _mapper.Map<Ability>(characterDboUpdated);
            return result;
        }

        public async Task DeleteAbility(long id)
        {
            await _unitOfWork.Ability.Delete(id);
        }
    }
}
