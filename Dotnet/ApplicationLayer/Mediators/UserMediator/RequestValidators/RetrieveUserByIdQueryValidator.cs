using DotNet6Mediator.ApplicationLayer.Mediators.UserMediators.Queries;
using FluentValidation;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.RequestValidators
{
    public class RetrieveUserByIdQueryValidator : AbstractValidator<RetrieveUserByIdQuery>
    {
        public RetrieveUserByIdQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
