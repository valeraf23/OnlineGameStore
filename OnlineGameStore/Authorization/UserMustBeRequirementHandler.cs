using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineGameStore.Api.Authorization.AccessRights;
using OnlineGameStore.Api.Services;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Roles;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Api.Authorization
{
    public class UserMustBeRequirementHandler : AuthorizationHandler<UserMustBeRequirementRole>
    {
        private readonly IGameService _gameService;
        private readonly IUserInfoService _userInfoService;

        public UserMustBeRequirementHandler(IUserInfoService userInfoService, IGameService gameService)
        {
            _userInfoService = userInfoService;
            _gameService = gameService;
        }

        private Either<NotAllowed, Allowed> IfAdmin(UserMustBeRequirementRole requirement)
        {
            if (_userInfoService.Roles.All(y => y != UserRoles.Admin)) return new NotAllowed();

            return new Allowed();
        }

        private Either<NotAllowed, Allowed> IfCreator(AuthorizationHandlerContext context)
        {
            if (!(context.Resource is AuthorizationFilterContext filterContext)) return new NotAllowed();

            var gameId = filterContext.RouteData.Values["id"].ToString();

            if (!Guid.TryParse(gameId, out var idAsGuid)) return new NotAllowed();

            if (!Guid.TryParse(_userInfoService.UserId, out var userIdAsGuid)) return new NotAllowed();

            if (!_gameService.IsGameOwner(idAsGuid, userIdAsGuid).Result) return new NotAllowed();

            return new Allowed();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserMustBeRequirementRole requirement)
        {
            IfAdmin(requirement)
                .DoWhenRight(() => context.Succeed(requirement))
                .DoWhenLeft(() =>
                    IfCreator(context)
                        .DoWhenRight(() => context.Succeed(requirement))
                        .DoWhenLeft(context.Fail));
            return Task.CompletedTask;
        }
    }
}