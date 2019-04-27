using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Data.Dtos
{
    public class PublisherModel : IModel
    {
        [Required(ErrorMessage = "You should fill out a Name.")]
        [MaxLength(50, ErrorMessage = "The Name shouldn't have more than 50 characters.")]
        public string Name { get; set; }

        public Guid Id { get; set; }
    }
}