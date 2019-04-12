using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Data.Dtos
{
    public class PublisherModel : IModel
    {
        [Required(ErrorMessage = "You should fill out a Name.")]
        [MaxLength(50, ErrorMessage = "The Name shouldn't have more than 50 characters.")]
        public string Name { get; set; }

        public virtual ICollection<Guid> GamesId { get; set; }
            = new List<Guid>();

        public Guid Id { get; set; }
    }
}