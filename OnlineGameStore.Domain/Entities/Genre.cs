using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Domain.Entities
{
    public class Genre
    {
        [Key] public int Id { get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; }

        public virtual ICollection<GameGenre> GameGenre { get; set; } = new List<GameGenre>();
        public int? ParentId { get; set; }
        public virtual Genre ParentGenre { get; set; }
        public virtual ICollection<Genre> SubGenres { get; set; } = new List<Genre>();
    }
}
