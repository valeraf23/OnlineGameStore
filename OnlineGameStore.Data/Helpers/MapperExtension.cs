using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Helpers
{
    public static class MapperExtension
    {
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