using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineGame.DataAccess.Interfaces;

namespace OnlineGameStore.Domain.Entities
{
    public class Genre : IGuidIdentity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }

        public virtual ICollection<GameGenre> GameGenre { get; set; } = new List<GameGenre>();
        public Guid? ParentId { get; set; }
        public virtual Genre ParentGenre { get; set; }
        public virtual ICollection<Genre> SubGenres { get; set; } = new List<Genre>();
        [Key] public Guid Id { get; set; }
    }
}