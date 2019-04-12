using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineGame.DataAccess;

namespace OnlineGameStore.Domain.Entities
{
    public class PlatformType : IGuidIdentity
    {
        public virtual ICollection<GamePlatformType> Games { get; set; } = new List<GamePlatformType>();
        public string Type { get; set; }

        [Key] public Guid Id { get; set; }
    }
}