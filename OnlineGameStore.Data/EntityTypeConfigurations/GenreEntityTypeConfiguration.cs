using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.EntityTypeConfigurations
{
    internal class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .HasMany(u => u.SubGenres)
                .WithOne(p => p.ParentGenre)
                .HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => new {p.Name})
                .IsUnique();
        }
    }
}