using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface ICharacterService
    {
        Task<CharacterCreateResponse> CreateCharacter(CharacterCreateRequest requestObject);
        Task<CharacterGetResponse> GetCharacterById(CharacterGetRequest requestObject);
        Task<AllCharacter> GetAllCharacters();
        Task<CharacterUpdateResponse> UpdateCharacter(CharacterUpdateRequest requestObject);
        Task DeleteCharacter(CharacterDeleteRequest requestObject);
        Task<CharacterAddGroupResponse> CharacterAddGroup(CharacterAddGroupRequest requestObject);
        Task<CharacterLevelBoostResponse> CharacterLevelBoost(CharacterLevelBoostRequest requestObject);
        Task<CharacterAddAbilitiesResponse> CharacterAddAbilities(CharacterAddAbilitiesRequest requestObject);
    }
}
