using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private OdysseyDbContext _context;

        public UnitOfWork(OdysseyDbContext context)
        {
            _context = context;
            Ability = new AbilityRepository(_context);
            Character = new CharacterRepository(_context);
        }
        public IAbilityRepository Ability { get; private set; }
        public ICharacterRepository Character { get; private set; }
    }
}
