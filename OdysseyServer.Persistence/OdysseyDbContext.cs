using Microsoft.EntityFrameworkCore;
using OdysseyServer.Persistence.Configurations;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OdysseyServer.Persistence
{
    public class OdysseyDbContext : DbContext
    {
        public OdysseyDbContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Ability> Abilities { get; set; }
        public virtual DbSet<CharacterAbilities> CharacterAbilities { get; set; }
        public virtual DbSet<AbilityStat> AbilityStat { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<CharacterGroups> CharacterGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CharacterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AbilityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CharacterAbilityConfiguration());
            modelBuilder.ApplyConfiguration(new AbilityStatConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new CharacterGroupConfiguration());

            //modelBuilder.Seed();
        }
    }
}
