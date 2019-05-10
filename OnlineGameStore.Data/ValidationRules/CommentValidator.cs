using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class CommentValidator : AbstractValidator<CommentModel>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Name).Length(3, 50);
            RuleFor(x => x.Body).Length(3, 500);
        }
    }
}