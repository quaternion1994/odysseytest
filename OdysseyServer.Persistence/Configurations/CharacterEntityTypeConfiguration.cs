using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;

namespace OdysseyServer.Persistence.Configurations
{
    internal class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<CharacterDbo>
    {
        public void Configure(EntityTypeBuilder<CharacterDbo> builder)
        {
            builder.ToTable("Character");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Level).HasColumnName("Level");
            builder.Property(e => e.Power).HasColumnName("Power");
            builder.Property(e => e.GearTier).HasColumnName("GearTier");
            builder.Property(e => e.Xp).HasColumnName("Xp");
            builder.Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
