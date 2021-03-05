using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    public class CharacterGroupConfiguration : IEntityTypeConfiguration<CharacterGroups>
    {
        public void Configure(EntityTypeBuilder<CharacterGroups> builder)
        {
            builder.ToTable("CharacterGroups");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.HasOne(d => d.Character)
                    .WithMany()
                    .HasForeignKey(t => t.CharacterId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CharacterGroups_Characters")
                    .IsRequired();

            builder.HasOne(d => d.Group)
                        .WithMany()
                        .HasForeignKey(t => t.GroupId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_CharacterGroups_Group")
                        .IsRequired();
        }
        
    }
}
