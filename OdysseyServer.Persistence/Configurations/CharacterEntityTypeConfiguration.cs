using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdysseyServer.Persistence.Entities;

namespace OdysseyServer.Persistence.Configurations
{
    internal class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Characters").HasKey(k => k.Id);

            builder.HasMany(e => e.Abilities)
                .WithOne(e => e.Character)
                .HasForeignKey(p => p.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
