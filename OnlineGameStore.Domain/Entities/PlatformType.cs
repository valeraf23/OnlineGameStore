using System.Collections.Generic;

namespace OnlineGameStore.Domain.Entities
{
    public class PlatformType : Entity
    {
        public virtual ICollection<GamePlatformType> Games { get; set; } = new List<GamePlatformType>();
        public string Type { get; set; }
    }
}