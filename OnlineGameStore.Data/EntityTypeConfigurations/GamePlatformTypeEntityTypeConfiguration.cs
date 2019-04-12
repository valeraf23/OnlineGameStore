using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.EntityTypeConfigurations
{
    internal class GamePlatformTypeEntityTypeConfiguration : IEntityTypeConfiguration<GamePlatformType>
    {
        public void Configure(EntityTypeBuilder<GamePlatformType> builder)
        {
            builder
                .HasKey(pc => new {pc.GameId, pc.PlatformTypeId});

            builder
                .HasOne(pc => pc.Game)
                .WithMany(p => p.GamePlatformType)
                .HasForeignKey(pc => pc.GameId);

            builder
                .HasOne(pc => pc.PlatformType)
                .WithMany(c => c.Games)
                .HasForeignKey(pc => pc.PlatformTypeId);
        }



    }
}