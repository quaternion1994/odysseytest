using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class LevelExperienceConfiguration : IEntityTypeConfiguration<LevelExperienceDbo>
    {
        public void Configure(EntityTypeBuilder<LevelExperienceDbo> builder)
        {
            builder.ToTable("LevelExperience");
            builder.HasNoKey();
            builder.Property(e => e.Level).HasColumnName("Level");
            builder.Property(e => e.ExperienceForUp).HasColumnName("ExperienceForUp");
        }
    }
}
