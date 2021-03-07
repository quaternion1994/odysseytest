using OdysseyServer.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface IAbilityService
    {
        Task CreateAbility(Ability ability);
        Task<Ability> GetAbilityById(long Id);
        Task<AllAbility> GetAllAbilities();
        Task<Ability> UpdateAbility(Ability ability);
        Task DeleteAbility(long id);
    }
}
