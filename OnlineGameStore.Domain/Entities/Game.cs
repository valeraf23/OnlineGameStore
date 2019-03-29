using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGameStore.Domain.Entities
{
    public class Game
    {

        [Key] public int Id { get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; }

        [Required] public string Description { get; set; }

        [ForeignKey("PublisherId")] public Publisher Publisher { get; set; }

        public int PublisherId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<GameGenre> GameGenre { get; set; } = new List<GameGenre>();
        public virtual ICollection<GamePlatformType> GamePlatformType { get; set; } = new List<GamePlatformType>();
    }
}