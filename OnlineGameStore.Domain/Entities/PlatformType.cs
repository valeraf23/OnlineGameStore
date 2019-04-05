using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Domain.Entities
{
    public class PlatformType
    {
        public virtual ICollection<GamePlatformType> GamePlatformType { get; set; } = new List<GamePlatformType>();

        [Key] public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
