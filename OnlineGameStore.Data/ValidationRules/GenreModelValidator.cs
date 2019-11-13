using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class GenreModelValidator : AbstractValidator<GenreModel>
    {
        public GenreModelValidator()
        {
            RuleFor(x => x.Name).Length(5, 50);
        }
    }
}