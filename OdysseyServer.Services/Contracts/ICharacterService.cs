using OdysseyServer.ApiClient;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface ICharacterService
    {
        Task<CharacterCreateResponse> CreateCharacterAsync(CharacterCreateRequest requestObject);
        Task<CharacterGetResponse> GetCharacterByIdAsync(long characterId);
        Task<CharacterAllResponse> GetAllCharactersAsync();
        Task<CharacterUpdateResponse> UpdateCharacterAsync(CharacterUpdateRequest requestObject);
        Task DeleteCharacterAsync(long characterId);
        Task<CharacterAddGroupResponse> CharacterAddGroupAsync(CharacterAddGroupRequest requestObject);
        Task<CharacterLevelBoostResponse> CharacterLevelBoostAsync(CharacterLevelBoostRequest requestObject);
        Task<CharacterAbilityBoostResponse> CharacterBoostAbilitiesAsync(CharacterAbilityBoostRequest requestObject);
    }
}
