using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class PlatformTypeModelValidator : AbstractValidator<PlatformTypeModel>
    {
        public PlatformTypeModelValidator()
        {
            RuleFor(x => x.Type).Length(3, 50);
        }
    }
}