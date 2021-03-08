using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class CharacterAbilityConfiguration : IEntityTypeConfiguration<CharacterAbilitiesDbo>
    {
        public void Configure(EntityTypeBuilder<CharacterAbilitiesDbo> builder)
        {
            builder.ToTable("CharacterAbilities");

            builder.HasKey(ca => new { ca.AbilityId, ca.CharacterId });
            builder.Property(a => a.RowVersion).IsRowVersion();

            builder.HasOne(d => d.Character)
                .WithMany(x => x.CharacterAbilities)
                .HasForeignKey(t => t.CharacterId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CharacterAbilities_Characters")
                .IsRequired();

            builder.HasOne(d => d.Ability)
                    .WithMany(x => x.CharacterAbilities)
                    .HasForeignKey(t => t.AbilityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CharacterAbilities_Ability")
                    .IsRequired();
        }
        
    }
}
