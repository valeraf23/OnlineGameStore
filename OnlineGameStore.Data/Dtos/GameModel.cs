using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Data.Dtos
{
    public class GameModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "You should fill out a Name.")]
        [MaxLength(50, ErrorMessage = "The Name shouldn't have more than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill out a Description.")]
        [MaxLength(500, ErrorMessage = "The Name shouldn't have more than 500 Description.")]
        public string Description { get; set; }

        public PublisherModel Publisher { get; set; }

        public virtual ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();

        public virtual ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public virtual ICollection<PlatformTypeModel> PlatformTypes { get; set; } = new List<PlatformTypeModel>();
    }
}