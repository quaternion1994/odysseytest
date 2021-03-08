using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Configurations
{
    internal class GroupConfiguration : IEntityTypeConfiguration<GroupDbo>
    {
        public void Configure(EntityTypeBuilder<GroupDbo> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.IconName).HasColumnName("IconName");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
