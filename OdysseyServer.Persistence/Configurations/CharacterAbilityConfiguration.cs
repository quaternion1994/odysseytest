using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class CharacterAbilityConfiguration : IEntityTypeConfiguration<CharacterAbilities>
    {
        public void Configure(EntityTypeBuilder<CharacterAbilities> builder)
        {
            builder.ToTable("CharacterAbilities");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.HasOne(d => d.Character)
                .WithMany()
                .HasForeignKey(t => t.CharacterId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CharacterAbilities_Characters")
                .IsRequired();

            builder.HasOne(d => d.Ability)
                    .WithMany()
                    .HasForeignKey(t => t.AbilityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CharacterAbilities_Ability")
                    .IsRequired();
        }
        
    }
}
