using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OnlineGameStore.Common.Optional;
using OnlineGameStore.Common.Optional.Extensions;

namespace OnlineGameStore.Data.Dtos
{
    public class DefaultValidatorStrategy<T> : IValidatorStrategy<T>
    {
        public Option<List<ValidationResult>> GetResults(T model)
        {
            var results = new List<ValidationResult>();

            var context = new ValidationContext(model);

            Validator.TryValidateObject(model, context, results, true);

            return results.When(x => x.Any());
        }
    }
}
