using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface IUnitOfWork
    {
        IAbilityRepository Ability { get; }
        ICharacterRepository Character { get; }
        IGroupRepository Group { get; }
        Task SaveChangesAsync();
    }
}
