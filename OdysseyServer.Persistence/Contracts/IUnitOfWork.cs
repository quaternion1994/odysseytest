using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Contracts
{
    public interface IUnitOfWork
    {
        IAbilityRepository Ability { get; }
        ICharacterRepository Character { get; }
        ICharacterAbilitiesRepository CharacterAbilities { get; }
    }
}
