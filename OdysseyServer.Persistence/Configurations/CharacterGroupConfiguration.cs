using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    public class CharacterGroupConfiguration : IEntityTypeConfiguration<CharacterGroupsDbo>
    {
        public void Configure(EntityTypeBuilder<CharacterGroupsDbo> builder)
        {
            builder.ToTable("CharacterGroups");
            builder.Property(a => a.RowVersion).IsRowVersion();

            builder.HasKey(x => new { x.CharacterId, x.GroupId});

            builder.HasOne(d => d.Character)
                    .WithMany(x => x.CharacterGroups)
                    .HasForeignKey(t => t.CharacterId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CharacterGroups_Characters")
                    .IsRequired();

            builder.HasOne(d => d.Group)
                        .WithMany(x => x.CharacterGroups)
                        .HasForeignKey(t => t.GroupId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_CharacterGroups_Group")
                        .IsRequired();
        }
        
    }
}
