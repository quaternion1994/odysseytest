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
        Task<CharacterGetResponse> GetCharacterById(long characterId);
        Task<CharacterAllResponse> GetAllCharacters();
        Task<CharacterUpdateResponse> UpdateCharacter(CharacterUpdateRequest requestObject);
        Task DeleteCharacter(long characterId);
        Task<CharacterAddGroupResponse> CharacterAddGroup(CharacterAddGroupRequest requestObject);
        Task<CharacterLevelBoostResponse> CharacterLevelBoost(CharacterLevelBoostRequest requestObject);
        Task<CharacterAbilityBoostResponse> CharacterBoostAbilities(CharacterAbilityBoostRequest requestObject);
    }
}
