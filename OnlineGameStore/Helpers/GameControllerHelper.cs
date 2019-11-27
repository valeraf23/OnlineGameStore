using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using OnlineGameStore.Api.Helpers.GameFilterStrategies;
using OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions;
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
            var filters = new BaseFilterStrategy[]
            {
                new GenreFilterStrategy(gameResourceParameters), new PlatformTypeFilterStrategy(gameResourceParameters),
                new PublisherFilterStrategy(gameResourceParameters), new SearchFilterStrategy(gameResourceParameters)
            };
            return filters.Aggregate(models, (current, filter) => filter.ApplyFilter(current));
        }

        public string GetPaginationMetadata(PagedList<GameModel> pages,
            GameResourceParameters gameResourceParameters, HttpContext httpContext)
        {
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
            var f = GetLink(httpContext, gameResourceParameters);
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return f(n => n - 1);
                case ResourceUriType.NextPage:
                    return f(n => n + 1);
                default:
                    return f(n => n);
            }
        }

        private Func<Func<int, int>, string> GetLink(HttpContext httpContext,
            GameResourceParameters gameResourceParameters, string action = "GetGames")
        {
            return getPageNumber => _linkGenerator.GetPathByAction(httpContext, action, values:
                new
                {
                    fields = gameResourceParameters.Fields,
                    orderBy = gameResourceParameters.OrderBy,
                    searchQuery = gameResourceParameters.SearchQuery,
                    genre = gameResourceParameters.Genre,
                    pageNumber = getPageNumber(gameResourceParameters.PageNumber),
                    pageSize = gameResourceParameters.PageSize
                });

        }
    }
}
