using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Filters
{
    public class AssignPublisherIdAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var baseController = context.Controller as ControllerBase;
            foreach (var argument in context.ActionArguments.Values.Where(v => v is GameForCreationModel))
            {
                var model = (GameForCreationModel) argument;
                // var userId = baseController.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.PublisherId = Guid.NewGuid();
            }

            await next();
        }
    }
}

