using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineGameStore.Api.Filters.PublisherIdStrategies;
using OnlineGameStore.Api.Helpers;
using OnlineGameStore.Api.Services;

namespace OnlineGameStore.Api.Filters
{
    public abstract class BaseAssignModelAttribute : ActionFilterAttribute
    {
        private readonly IUserInfoService _userInfoService;
        protected Type[] Models;
        private static IEnumerable<IAssignPublisherIdStrategy> Strategies => PublisherIdStrategiesHelper.Get();

        protected BaseAssignModelAttribute(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
            Models = new Type[] { };
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!Guid.TryParse(_userInfoService.UserId, out var userIdAsGuid))
            {
                context.Result = new BadRequestObjectResult($"{userIdAsGuid} is not valid for UserId");
                return;
            }

            var notSupportedTypes = Models.Where(IsNotSupportedType).ToArray();
            if (notSupportedTypes.Any())
            {
                throw new NotSupportedException(notSupportedTypes.Aggregate(new StringBuilder(),
                    (s, t) => s.AppendLine($"{t} type does not supported, please add Strategy"), sb => sb.ToString()));
            }

            var supportStrategies = Strategies.Where(s => IsSupportStrategy(s.ForType)).ToArray();

            foreach (var argument in context.ActionArguments.Values.Where(v =>
                supportStrategies.Any(s => s.ForType == v.GetType())))
            {
                supportStrategies.Do(x => x.Convert(argument, userIdAsGuid));
            }

            await next();
        }

        private static bool IsNotSupportedType(Type type) => Strategies.All(s => s.ForType != type);
        private bool IsSupportStrategy(Type type) => Models.Any(m => m == type);
    }
}