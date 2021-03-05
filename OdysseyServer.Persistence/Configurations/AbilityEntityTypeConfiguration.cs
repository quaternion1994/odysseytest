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
            builder.ToTable("Ability");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Level).HasColumnName("Level");

            builder.HasOne(x => x.AbilityStat)
                .WithMany(x => x.Abilities)
                .HasConstraintName("FK_Ability_AbilityStat");
        }
    }
}
