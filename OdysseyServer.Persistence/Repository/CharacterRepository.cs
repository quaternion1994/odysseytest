﻿using Microsoft.EntityFrameworkCore;
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

        public async Task CharacterLevelBoostAsync(long id, int lvlNumber)
        {
            var entityToModified = await base.GetByID(id);
            var leverExperienceList = await _context.LevelExperience.Where(x => x.Level > entityToModified.Level & x.Level <= lvlNumber + entityToModified.Level).ToListAsync();
            var newCurrentExperience = entityToModified.Xp + leverExperienceList.Sum(x => x.ExperienceForUp);
            entityToModified.Level = entityToModified.Level + lvlNumber;
            var nextLEvel = entityToModified.Level + 1;
            var newMaxExpirience = await _context.LevelExperience.Where(x => x.Level == nextLEvel).FirstOrDefaultAsync();
            var statsForLevel = await _context.LevelStats.FirstOrDefaultAsync(x => x.LevelNumber == entityToModified.Level);
            entityToModified.Health = statsForLevel.Health;
            entityToModified.Offence = statsForLevel.Offence;
            entityToModified.Defence = statsForLevel.Defence;
            entityToModified.Power = (statsForLevel.Offence + statsForLevel.Defence + statsForLevel.Health / 10) / 3;
            entityToModified.Xp = newCurrentExperience;
            entityToModified.XpToNextLevel = entityToModified.Xp + newMaxExpirience.ExperienceForUp;
            await base.Update(entityToModified);
        }

        public async Task CharacterAbilityBoostAsync(long characterId, long abilityId)
        {
            var entityToModified = await this.GetCharacterByIdAsync(characterId);
            var currentAbility = entityToModified.Abilities.FirstOrDefault(x => x.Id == abilityId);
            var abilityEntity = await _context.Abilities.FirstOrDefaultAsync(x => x.AbilityType == currentAbility.AbilityType & x.Level == currentAbility.Level + 1);
            entityToModified.Abilities.Add(abilityEntity);           
            await base.Update(entityToModified);
        }

        public async Task<CharacterDbo> GetCharacterByIdAsync(long id)
        {
            return await dbSet.Include(x => x.Abilities).ThenInclude(x => x.Stats).Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CharacterDbo>> GetAllCharactersAsync()
        {
            return await dbSet.Include(x => x.Abilities).ThenInclude(x => x.Stats).Include(x => x.Groups).AsNoTracking().ToListAsync();
        }
        public async Task UnlockInitialAbilityAsync(long characterId, int characterLevel)
        {
            var abilityForUnlock = new List<AbilityDbo>();
            var lockedAbility = await _context.Abilities.Where(x => x.RequiredLevel <= characterLevel && x.RequiredLevel > 1).ToListAsync();
            var characterAbility = await _context.Characters.Where(x => x.Id == characterId).Select(x => x.Abilities).FirstOrDefaultAsync();
            foreach (var elem in lockedAbility.OrderBy(x => x.Level))
            {
                if (!characterAbility.Any(x => x.Id == elem.Id) && elem.Level == 1)
                {
                    abilityForUnlock.Add(elem);
                }
            }
            var entityToModified = await base.GetByID(characterId);
            foreach (var elem in abilityForUnlock)
            {
                entityToModified.Abilities.Add(elem);
            }           
            await base.Update(entityToModified);
        }
    }
}
