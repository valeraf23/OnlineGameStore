using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Data.Dtos
{
    public class DefaultValidatorStrategy<T> : IValidatorStrategy<T>
    {
        public bool IsValid(T validateThis)
        {
            var results = GetResults(validateThis);

            return results.Count == 0;
        }

        private IList<ValidationResult> GetResults(T model)
        {
            var results = new List<ValidationResult>();

            var context = new ValidationContext(model);

            Validator.TryValidateObject(
                model, context, results, true);

            return results;
        }
    }
}
