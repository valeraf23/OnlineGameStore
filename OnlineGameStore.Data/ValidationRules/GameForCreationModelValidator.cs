using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class GameForCreationModelValidator : AbstractValidator<GameForCreationModel>
    {
        public GameForCreationModelValidator()
        {
            RuleFor(x => x.Name).Length(0, 50);
            RuleFor(x => x.Description).Length(3, 500);
            RuleFor(x => x.GenresId).NotEmpty();
            RuleFor(x => x.PlatformTypesId).NotEmpty();
        }
    }
}