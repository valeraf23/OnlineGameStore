using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class PublisherValidator : AbstractValidator<PublisherModel>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Name).Length(4, 50);
        }
    }
}