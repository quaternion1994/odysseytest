using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Repository
{
    public class AbilityRepository : Repository<Ability>, IAbilityRepository
    {
        private OdysseyDbContext _context;

        public AbilityRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }     
    }
}
