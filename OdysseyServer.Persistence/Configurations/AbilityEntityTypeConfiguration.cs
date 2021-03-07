using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;

namespace OdysseyServer.Persistence.Configurations
{
    internal class AbilityEntityTypeConfiguration : IEntityTypeConfiguration<AbilityDbo>
    {
        public void Configure(EntityTypeBuilder<AbilityDbo> builder)
        {
            builder.ToTable("Ability");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            builder.Property(x => x.RequiredLevel).HasColumnName("RequiredLevel");
            builder.Property(e => e.Level).HasColumnName("Level");
            builder.Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
