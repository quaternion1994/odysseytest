using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class AbilityStatConfiguration : IEntityTypeConfiguration<AbilityStat>
    {
        public void Configure(EntityTypeBuilder<AbilityStat> builder)
        {
            builder.ToTable("AbilityStat");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Attack).HasColumnName("Attack");
            builder.Property(e => e.Defence).HasColumnName("Level");
        }
    }
}
