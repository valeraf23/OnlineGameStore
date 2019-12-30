using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Domain.Entities
{
    public class Genre : Entity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }

        public virtual ICollection<GameGenre> GameGenre { get; set; } = new List<GameGenre>();
        public Guid? ParentId { get; set; }
        public virtual Genre ParentGenre { get; set; }
        public virtual ICollection<Genre> SubGenres { get; set; } = new List<Genre>();
    }
}