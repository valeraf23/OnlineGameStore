using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineGame.DataAccess;

namespace OnlineGameStore.Domain.Entities
{
    public class Publisher : IGuidIdentity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
            = new List<Game>();

        [Key] public Guid Id { get; set; }
    }
}