using FluentValidation;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.ValidationRules
{
    public class PublisherForCreateModelValidator : AbstractValidator<PublisherForCreateModel>
    {
        public PublisherForCreateModelValidator()
        {
            RuleFor(x => x.Name).Length(4,10);
        }
    }
}