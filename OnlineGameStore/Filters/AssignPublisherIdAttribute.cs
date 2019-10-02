using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineGameStore.Api.Services;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Filters
{
    public class AssignPublisherIdAttribute : ActionFilterAttribute
    {
        private readonly IUserInfoService _userInfoService;

        public AssignPublisherIdAttribute(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values.Where(v => v is GameForCreationModel))
            {
                var model = (GameForCreationModel) argument;

                if (!Guid.TryParse(_userInfoService.UserId, out var userIdAsGuid))
                {
                    context.Result = new BadRequestObjectResult($"{userIdAsGuid} is not valid for UserId");
                    return;
                }

                model.PublisherId = userIdAsGuid;
            }

            await next();
        }
    }
}

