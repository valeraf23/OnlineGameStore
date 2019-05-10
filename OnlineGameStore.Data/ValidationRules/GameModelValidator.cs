using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class GameModelValidator : AbstractValidator<GameModel>
    {
        public GameModelValidator()
        {
            RuleFor(x => x.Name).Length(5, 50);
            RuleFor(x => x.Description).Length(3, 500);
        }
    }
}