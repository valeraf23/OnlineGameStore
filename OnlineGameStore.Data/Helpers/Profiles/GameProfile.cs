using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace OnlineGameStore.Data.Helpers.Profiles
{
    public class GameProfile:Profile
    {
        public GameProfile()
        {
            cfg.CreateMap<Game, GameModel>()
                    .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                        src.GameGenre.Select(x => x.Genre).ToList()))
                    .ForMember(dest => dest.PlatformTypes, opt => opt.MapFrom(src =>
                        src.GamePlatformType.Select(x => x.PlatformType).ToList()));
            cfg.CreateMap<Comment, CommentModel>();
            cfg.CreateMap<CommentModel, Comment>();

            cfg.CreateMap<GameModel, Game>().ForMember(dest => dest.GameGenre, opt => opt.MapFrom(src =>
                    src.Genres))
                .ForMember(dest => dest.GamePlatformType, opt => opt.MapFrom(src => src.PlatformTypes));


            cfg.CreateMap<Genre, GenreModel>().ForMember(dest => dest.GamesId,
                opt => opt.MapFrom(src => src.GameGenre.Select(g => g.GameId).ToList()));

            cfg.CreateMap<PlatformType, PlatformTypeModel>().ForMember(dest => dest.GamesId,
                opt => opt.MapFrom(src => src.Games.Select(g => g.GameId).ToList()));

            cfg.CreateMap<Publisher, PublisherModel>();
            cfg.CreateMap<PublisherModel, Publisher>();

            cfg.CreateMap<GameForCreationModel, GameModel>().ForMember(dest => dest.Publisher,
                opt => opt.MapFrom(src => new PublisherModel
                {
                    Id = src.PublisherId
                })).ForMember(dest => dest.PlatformTypes,
                opt => opt.MapFrom(src => src.PlatformTypesId.Select(guid => new PlatformTypeModel
                {

                    Id = guid
                })));

            cfg.CreateMap<GenreModel, Genre>();
            cfg.CreateMap<GenreModel, GameGenre>().ConstructUsing(dest => new GameGenre
            {
                Genre = Mapper.Map<Genre>(dest)
            });

            cfg.CreateMap<PlatformTypeModel, PlatformType>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));

            cfg.CreateMap<PlatformTypeModel, GamePlatformType>().ForMember(dest => dest.PlatformType,
                opt => opt.MapFrom(x => new PlatformType { Type = x.Type }));

            cfg.CreateMap<PublisherModel, Publisher>();
            cfg.CreateMap<PublisherForCreateModel, Publisher>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
        }
    }
}
