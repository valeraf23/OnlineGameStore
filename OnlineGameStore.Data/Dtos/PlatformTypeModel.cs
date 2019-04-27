using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Data.Dtos
{
    public class PlatformTypeModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "You should fill out a Type.")]
        [MaxLength(50, ErrorMessage = "The Type shouldn't have more than 50 characters.")]
        public string Type { get; set; }
    }
}
