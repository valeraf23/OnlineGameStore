using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using OnlineGameStore.Common.Errors;

namespace OnlineGameStore.Data.Helpers
{
    public static class ValidationResultsExtension
    {
        public static Task<UnprocessableError> GetUnprocessableError(this IList<ValidationResult> errors) =>
            Task.Run(() => new UnprocessableError(errors));
    }
}
