using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface IAbilityRepository : IRepository<AbilityDbo>
    {
        Task<AbilityDbo> GetByIdAsync(long id);
        Task<List<AbilityDbo>> GetByArrayIdAsync(List<long> idsLIst);
        Task<List<AbilityDbo>> GetInitialAbilityAsync();
    }
}
