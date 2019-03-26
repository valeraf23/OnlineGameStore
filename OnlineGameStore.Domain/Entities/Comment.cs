using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGameStore.Domain.Entities
{
    public class Comment
    {
        [Key] public int Id { get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; }

        [Required] public string Body { get; set; }

        [ForeignKey("GameId")] public Game Game { get; set; }

        public int GameId { get; set; }

        public int? ParentId { get; set; }
        public Comment ParentComment { get; set; }
        public virtual ICollection<Comment> Answers { get; set; } = new List<Comment>();
    }
}