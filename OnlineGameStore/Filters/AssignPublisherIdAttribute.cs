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
                // var userId =get ClaimTypes from baseController
                model.PublisherId = new Guid("e99d98ab-56d2-400a-93eb-65d731d9144d");
            }

            await next();
        }
    }
}

