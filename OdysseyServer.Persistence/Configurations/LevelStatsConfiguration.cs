using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class LevelStatsConfiguration : IEntityTypeConfiguration<LevelStatsDbo>
    {
        public void Configure(EntityTypeBuilder<LevelStatsDbo> builder)
        {
            builder.ToTable("LevelStats");
            builder.Property(e => e.LevelNumber).HasColumnName("LevelNumber");
            builder.Property(e => e.Defence).HasColumnName("Defence");
            builder.Property(e => e.Health).HasColumnName("Health");
            builder.Property(a => a.Offence).HasColumnName("Offence");
            builder.HasNoKey();
        }
    }
}
