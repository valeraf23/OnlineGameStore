using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineGameStore.Common.Errors;

namespace OnlineGameStore.Api.Helpers
{
    public static class ObjectResultExtension
    {
        public static IActionResult ToObjectResult(this ModelStateDictionary modelState) =>
            new UnprocessableEntityObjectResult(modelState);

        public static IActionResult ToObjectResult(this Error error)
        {
            switch (error)
            {
                case UnprocessableError e:
                    return new UnprocessableEntityObjectResult(e.Res);
                case SaveError e:
                    return new UnprocessableEntityObjectResult(e.ErrorMsg);
                default:
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
