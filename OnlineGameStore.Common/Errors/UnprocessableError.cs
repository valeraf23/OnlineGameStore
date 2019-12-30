using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OnlineGameStore.Common.Errors
{
    public class UnprocessableError : Error
    {
        public readonly IList<ValidationResult> Res;

        public UnprocessableError(IList<ValidationResult> res)
        {
            Res = res;
        }

        public UnprocessableError(){}

        public override string ToString()
        {
            return Res.Aggregate(new StringBuilder(), (s, er) =>
            {
                s.AppendJoin(",", er.MemberNames);
                s.Append(": ");
                s.AppendLine(er.ErrorMessage);
                return s;
            }, stb => stb.ToString());
        }
    }
}
