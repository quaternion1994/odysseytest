using OdysseyServer.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface IAbilityService
    {
        Task<AbilityAddResponse> CreateAbility(AbilityAddRequest requestObject);
        Task<AllAbility> GetAllAbilities();
        Task<AbilityUpdateResponse> UpdateAbility(AbilityUpdateRequest requestObject);
        Task DeleteAbility(AbilityDeleteRequest requestObject);
        Task<AbilityGetResponse> GetAbilityById(AbilityGetRequest requestObject);
    }
}
