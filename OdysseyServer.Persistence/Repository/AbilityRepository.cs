using Microsoft.EntityFrameworkCore;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Repository
{
    public class AbilityRepository : Repository<AbilityDbo>, IAbilityRepository
    {
        private OdysseyDbContext _context;

        public AbilityRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AbilityDbo> GetByID(long id)
        {
            return await dbSet.Include(x => x.Stats).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async virtual Task<List<AbilityDbo>> GetByArrayId(List<long> idsLIst)
        {
            return await dbSet.Where(x => idsLIst.Contains(x.Id)).Include(x => x.Stats).ToListAsync();
        }

        public async virtual Task<List<AbilityDbo>> GetInitialAbility()
        {
            return await dbSet.Where(x => x.RequiredLevel == 1).Include(x => x.Stats).ToListAsync();
        }
    }
}
