using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineGameStore.Common.Optional;

namespace OnlineGameStore.Data.Dtos
{
    public interface IValidatorStrategy<in T>
    {
        Option<List<ValidationResult>> GetResults(T model);
    }
}