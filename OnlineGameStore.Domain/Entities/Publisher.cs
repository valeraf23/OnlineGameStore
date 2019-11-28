using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Domain.Entities
{
    public class Publisher : Entity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
            = new List<Game>();
    }
}