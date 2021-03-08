using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface IAbilityRepository : IRepository<AbilityDbo>
    {
        Task<AbilityDbo> GetByID(long id);
        Task<List<AbilityDbo>> GetByArrayId(List<long> idsLIst);
    }
}
