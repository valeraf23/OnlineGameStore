using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineGameStore.Api.Helpers
{
    public class UnprocessableEntityObjectResult : ObjectResult
    {
        public UnprocessableEntityObjectResult(ModelStateDictionary modelState)
            : base(new SerializableError(modelState))
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            StatusCode = 422;
        }

        public UnprocessableEntityObjectResult(object error)
            : base(error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            StatusCode = 422;
        }
    }
}