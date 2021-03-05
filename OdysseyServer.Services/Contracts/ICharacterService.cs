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
    }
}
