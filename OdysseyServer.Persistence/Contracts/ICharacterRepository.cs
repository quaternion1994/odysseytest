using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface ICharacterRepository : IRepository<CharacterDbo>
    {
        Task CharacterLevelBoostAsync(long id, int lvlNumber);
        Task<CharacterDbo> GetCharacterByIdAsync(long id);
        Task<List<CharacterDbo>> GetAllCharactersAsync();
        Task CharacterAbilityBoostAsync(long characterId, long abilityId);
        Task UnlockInitialAbilityAsync(long characterId, int characterLevel);
    }
}
