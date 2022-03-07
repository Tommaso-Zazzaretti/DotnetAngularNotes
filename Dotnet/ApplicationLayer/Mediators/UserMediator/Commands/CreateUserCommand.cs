using DotNet6Mediator.ApplicationLayer.Attributes;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands
{
    [AuthorizationRequirement(Roles = "SuperAdmin")]
    public class CreateUserCommand:IRequest<UserDtoResponse?>
    {
        public UserDtoRequest UserToAdd { get; set; }
        public CreateUserCommand(UserDtoRequest NewUser)
        {
            this.UserToAdd = NewUser;
        }
    }
}
