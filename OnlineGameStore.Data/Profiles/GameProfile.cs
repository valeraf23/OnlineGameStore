using System.Linq;
using AutoMapper;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameModel>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.GameGenre.Select(x => x.Genre).ToList()))
                .ForMember(dest => dest.PlatformTypes, opt => opt.MapFrom(src =>
                    src.GamePlatformType.Select(x => x.PlatformType).ToList()))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));

            CreateMap<GameModel, Game>().ForMember(dest => dest.GameGenre, opt => opt.MapFrom(src =>
                    src.Genres))
                .ForMember(dest => dest.GamePlatformType, opt => opt.MapFrom(src => src.PlatformTypes))
                .ForMember(dest => dest.Publisher, opt => opt.Ignore());

            CreateMap<GameForCreationModel, GameModel>().ForMember(dest => dest.Publisher,
                    opt => opt.MapFrom(src => new PublisherModel
                    {
                        Id = src.PublisherId
                    })).ForMember(dest => dest.PlatformTypes,
                    opt => opt.MapFrom(src => src.PlatformTypesId.Select(guid => new PlatformTypeModel
                    {
                        Id = guid
                    })))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => src.GenresId.Select(x => new GenreModel
                    {
                        Id = x
                    })));
        }
    }
}