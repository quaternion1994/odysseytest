using OdysseyServer.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface IAbilityService
    {
        Task<AbilityAddResponse> CreateAbilityAsync(AbilityAddRequest requestObject);
        Task<AbilityAllResponse> GetAllAbilitiesAsync();
        Task<AbilityUpdateResponse> UpdateAbility(AbilityUpdateRequest requestObject);
        Task DeleteAbilityAsync(long abilityId);
        Task<AbilityGetResponse> GetAbilityByIdAsync(long abilityId);
    }
}
