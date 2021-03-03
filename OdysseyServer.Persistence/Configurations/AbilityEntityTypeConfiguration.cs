using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;

namespace OdysseyServer.Persistence.Configurations
{
    internal class AbilityEntityTypeConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.ToTable("Abilities").HasKey(k => k.Id);
        }
    }
}
