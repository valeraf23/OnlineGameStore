using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineGameStore.Common.Errors;

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