using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    public static class MapperHelper
    {
        public static void InitMapperConf()
        {
            Mapper.Initialize(cfg =>
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
                    opt => opt.MapFrom(src => src.Games.Select(g => g.GameId).ToList())).ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type)).ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id));

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
                    opt => opt.MapFrom(x => new PlatformType {Type = x.Type}));

                cfg.CreateMap<PublisherModel, Publisher>();
                cfg.CreateMap<PublisherForCreateModel, Publisher>().ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));


            });
        }

        public static TModel ToModel<TModel>(this IGuidIdentity entity) where TModel : IModel =>
            Mapper.Map<TModel>(entity);

        public static TEntity ToEntity<TEntity>(this IModel entity) where TEntity : IGuidIdentity =>
            Mapper.Map<TEntity>(entity);

        public static IList<TModel> ToModel<TModel>(this IEnumerable<IGuidIdentity> entity) where TModel : IModel =>
            Mapper.Map<IEnumerable<TModel>>(entity).ToList();

        public static IList<TEntity> ToEntity<TEntity>(this IEnumerable<IModel> entity)
            where TEntity : IGuidIdentity =>
            Mapper.Map<IEnumerable<TEntity>>(entity).ToList();
    }
}