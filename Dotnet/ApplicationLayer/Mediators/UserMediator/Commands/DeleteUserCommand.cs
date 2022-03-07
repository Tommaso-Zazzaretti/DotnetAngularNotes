using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands
{
    public class DeleteUserCommand: IRequest<UserDtoResponse?>
    {
        public int UserId { get; set; }
        public DeleteUserCommand(int Id)
        {
            this.UserId = Id;
        }
    }
    
}
