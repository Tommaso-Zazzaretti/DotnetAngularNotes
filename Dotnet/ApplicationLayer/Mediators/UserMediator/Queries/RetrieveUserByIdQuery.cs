using DotNet6Mediator.ApplicationLayer.Attributes;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediators.Queries
{
    //[AuthorizationRequirement(Roles = "Admin")]
    public class RetrieveUserByIdQuery:IRequest<UserDtoResponse?> {
        public int UserId { get; set; }

        public RetrieveUserByIdQuery(int Id)
        {
            this.UserId = Id;
        }
    }
}
