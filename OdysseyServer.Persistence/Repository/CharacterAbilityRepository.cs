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
    public class CharacterAbilityRepository : Repository<CharacterAbilitiesDbo>, ICharacterAbilitiesRepository
    {
        private OdysseyDbContext _context;

        public CharacterAbilityRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }

        public async virtual Task DeleteManyByCharacterId(long characterId)
        {
            var characterAbilities = await _context.CharacterAbilities.Where(x => x.CharacterId == characterId).ToListAsync();
            _context.CharacterAbilities.RemoveRange(characterAbilities);
            await context.SaveChangesAsync();
        }

        public async virtual Task DeleteGroupsByCharacterId(long characterId)
        {
            var characterGroups = await _context.CharacterGroups.Where(x => x.CharacterId == characterId).ToListAsync();
            _context.CharacterGroups.RemoveRange(characterGroups);
            await context.SaveChangesAsync();
        }
    }

}
