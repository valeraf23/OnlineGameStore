using AutoMapper;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreModel>();
            CreateMap<GenreModel, Genre>();
            CreateMap<GenreModel, GameGenre>().ForMember(dest => dest.GenreId,
                opt => opt.MapFrom(src => src.Id));
        }
    }
}