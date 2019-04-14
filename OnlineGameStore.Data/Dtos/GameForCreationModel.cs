using System;
using System.Collections.Generic;

namespace OnlineGameStore.Data.Dtos
{
    public class GameForCreationModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid PublisherId { get; set; }

        public virtual ICollection<GenreModel> GenresId { get; set; } = new List<GenreModel>();
        public virtual ICollection<Guid> PlatformTypesId { get; set; } = new List<Guid>();
    }
}
