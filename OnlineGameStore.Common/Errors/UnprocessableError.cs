using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Common.Errors
{
    public class UnprocessableError : Error
    {
        public readonly IList<ValidationResult> Res;

        public UnprocessableError(IList<ValidationResult> res)
        {
            Res = res;
        }

        public UnprocessableError()
        {

        }
    }
}
