using Microsoft.EntityFrameworkCore;
using OdysseyServer.Persistence.Configurations;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OdysseyServer.Persistence
{
    class OdysseyDbContext : DbContext
    {
        public OdysseyDbContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Ability> Abilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CharacterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AbilityEntityTypeConfiguration());

            modelBuilder.Seed();
        }
    }
}
