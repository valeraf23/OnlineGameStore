using System;

namespace OnlineGameStore.Domain.Entities
{
    public class GamePlatformType
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public Guid PlatformTypeId { get; set; }
        public PlatformType PlatformType { get; set; }
    }
}