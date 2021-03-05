using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Repository
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {
        private OdysseyDbContext _context;

        public CharacterRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
