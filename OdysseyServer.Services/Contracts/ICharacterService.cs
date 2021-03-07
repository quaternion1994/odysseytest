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
        Task CreateCharacter(Character character);
        Task<Character> GetCharacterById(long Id);
        Task<AllCharacter> GetAllCharacters();
        Task<Character> UpdateCharacter(Character character);
        Task DeleteCharacter(long id);
        Task<Character> CharacterLevelBoost(long id, int lvlNumber);
    }
}
