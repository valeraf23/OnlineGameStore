using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore.Api.Helpers
{
    public class GameControllerHelper
    {
        private readonly LinkGenerator _linkGenerator;

        public GameControllerHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public IEnumerable<GameModel> ApplyFilters(IEnumerable<GameModel> models,
            GameResourceParameters gameResourceParameters)
        {
            if (!string.IsNullOrEmpty(gameResourceParameters.SearchQuery))
            {
                var searchQueryForWhereClause = gameResourceParameters.SearchQuery
                    .Trim().ToLowerInvariant();

                models = models
                    .Where(a => a.Description.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                || a.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                || a.PlatformTypes.Any(x =>
                                    x.Type.ToLowerInvariant().Contains(searchQueryForWhereClause))
                                || a.Publisher.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }


            if (string.IsNullOrEmpty(gameResourceParameters.Genre)) return models;
            {
                var genreForWhereClause = gameResourceParameters.Genre
                    .Trim().ToLowerInvariant();

                models = models.Where(m => m.Genres.SelectMany(GetGenresName).Any(x =>
                    x.Equals(genreForWhereClause, StringComparison.CurrentCultureIgnoreCase))).ToList();
            }

            return models;
        }

        private static IList<string> GetGenresName(GenreModel models)
        {
            var list = new List<string> {models.Name.ToLowerInvariant()};
            foreach (var m in models.SubGenres) list.AddRange(GetGenresName(m));

            return list;
        }

        public string GetPaginationMetadata(IEnumerable<GameModel> gameModels,
            GameResourceParameters gameResourceParameters, HttpContext httpContext)
        {
            var pages = PagedList<GameModel>.Create(gameModels.ToList(), gameResourceParameters.PageNumber,
                gameResourceParameters.PageSize);


            var previousPageLink = pages.HasPrevious
                ? CreateGamesResourceUri(httpContext, gameResourceParameters,
                    ResourceUriType.PreviousPage)
                : null;

            var nextPageLink = pages.HasNext
                ? CreateGamesResourceUri(httpContext, gameResourceParameters,
                    ResourceUriType.NextPage)
                : null;

            var paginationMetadata = new
            {
                previousPageLink,
                nextPageLink,
                totalCount = pages.TotalCount,
                pageSize = pages.PageSize,
                currentPage = pages.CurrentPage,
                totalPages = pages.TotalPages
            };
            return JsonConvert.SerializeObject(paginationMetadata);
        }

        private string CreateGamesResourceUri(HttpContext httpContext, GameResourceParameters gameResourceParameters,
            ResourceUriType type)
        {
            const string action = "GetGames";
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _linkGenerator.GetPathByAction(httpContext, action, values:
                        new
                        {
                            fields = gameResourceParameters.Fields,
                            orderBy = gameResourceParameters.OrderBy,
                            searchQuery = gameResourceParameters.SearchQuery,
                            genre = gameResourceParameters.Genre,
                            pageNumber = gameResourceParameters.PageNumber - 1,
                            pageSize = gameResourceParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return _linkGenerator.GetPathByAction(httpContext, action, values:
                        new
                        {
                            fields = gameResourceParameters.Fields,
                            orderBy = gameResourceParameters.OrderBy,
                            searchQuery = gameResourceParameters.SearchQuery,
                            genre = gameResourceParameters.Genre,
                            pageNumber = gameResourceParameters.PageNumber + 1,
                            pageSize = gameResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetPathByAction(httpContext, action, values:
                        new
                        {
                            fields = gameResourceParameters.Fields,
                            orderBy = gameResourceParameters.OrderBy,
                            searchQuery = gameResourceParameters.SearchQuery,
                            genre = gameResourceParameters.Genre,
                            pageNumber = gameResourceParameters.PageNumber,
                            pageSize = gameResourceParameters.PageSize
                        });
            }
        }
    }
}
