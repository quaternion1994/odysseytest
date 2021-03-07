using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class AbilityStatConfiguration : IEntityTypeConfiguration<AbilityStatsDbo>
    {
        public void Configure(EntityTypeBuilder<AbilityStatsDbo> builder)
        {
            builder.ToTable("AbilityStats");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Attack).HasColumnName("Attack");
            builder.Property(e => e.Defence).HasColumnName("Defence");

            builder.HasOne(x => x.Ability)
                .WithOne(b => b.Stats)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<AbilityStatsDbo>(x => x.AbilityId)
                .HasConstraintName("FK_Ability_AbilityStats");
        }
    }
}
