using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using FluentValidation;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.RequestValidators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
