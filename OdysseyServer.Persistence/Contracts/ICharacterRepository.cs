using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface ICharacterRepository : IRepository<CharacterDbo>
    {
        Task CharacterLevelBoost(long id, int lvlNumber);
        Task<CharacterDbo> GetCharacterById(long id);
        Task<List<CharacterDbo>> GetAllCharacters();
        Task CharacterAbilityBoost(long characterId, long abilityId);
        Task UnlockInitialAbility(long characterId, int characterLevel);
    }
}
