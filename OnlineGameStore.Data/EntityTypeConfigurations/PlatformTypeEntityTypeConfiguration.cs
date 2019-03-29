using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    internal class PlatformTypeEntityTypeConfiguration : IEntityTypeConfiguration<PlatformType>
    {
        public void Configure(EntityTypeBuilder<PlatformType> builder) =>
            builder.HasIndex(p => new {p.Type})
                .IsUnique();
    }
}
