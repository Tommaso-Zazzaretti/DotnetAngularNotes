using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands
{
    public class UpdateUserCommand:IRequest<UserDtoResponse?>
    {
        public UserDtoRequest UserToUpdate { get; set; }
        public UpdateUserCommand(UserDtoRequest UpdateUser)
        {
            this.UserToUpdate = UpdateUser;
        }
    }
}
