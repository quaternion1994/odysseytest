using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface ICharacterAbilitiesRepository : IRepository<CharacterAbilitiesDbo>
    {
        Task DeleteManyByCharacterId(long characterId);
        Task DeleteGroupsByCharacterId(long characterId);
    }
}
