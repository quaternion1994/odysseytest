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
    public class CharacterRepository : Repository<CharacterDbo>, ICharacterRepository
    {
        private OdysseyDbContext _context;

        public CharacterRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CharacterLevelBoost(long id, int lvlNumber)
        {
            var entityToModified = await base.GetByID(id);

            entityToModified.Level = entityToModified.Level + lvlNumber;

            await base.Update(entityToModified);
        }

        public async Task<CharacterDbo> GetCharacterById(long id)
        {
            return await dbSet.Include(x => x.CharacterAbilities).ThenInclude(x => x.Ability).Include(x => x.CharacterGroups).ThenInclude(x => x.Group).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<CharacterDbo>> GetAllCharacters()
        {
            return await dbSet.Include(x => x.CharacterAbilities).ThenInclude(x => x.Ability).Include(x => x.CharacterGroups).ThenInclude(x => x.Group).ToListAsync();
        }
    }
}
