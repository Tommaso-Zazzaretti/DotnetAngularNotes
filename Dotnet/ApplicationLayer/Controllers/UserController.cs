using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using DotNet6Mediator.ApplicationLayer.Mediators.UserMediators.Queries;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6Mediator.ApplicationLayer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator Mediator)
        {
            this._mediator = Mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserDtoResponse? UserDtoById = await this._mediator.Send(new RetrieveUserByIdQuery(id));
            if(UserDtoById == null) { return NotFound(); }
            return Ok(UserDtoById);
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoRequest? NewUserData)
        {
            if(NewUserData == null) { return BadRequest("Body is null"); }
            UserDtoResponse? AddedUser = await this._mediator.Send(new CreateUserCommand(NewUserData));
            if (AddedUser == null) { return BadRequest("Error during create"); }
            return Ok(AddedUser);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDtoRequest? UserToUpdate)
        {
            if (UserToUpdate == null) { return BadRequest("Body is null"); }
            UserDtoResponse? UpdatedUser = await this._mediator.Send(new UpdateUserCommand(UserToUpdate));
            if (UpdatedUser == null) { return BadRequest("Error during update"); }
            return Ok(UpdatedUser);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            UserDtoResponse? DeletedUser = await this._mediator.Send(new DeleteUserCommand(id));
            if (DeletedUser == null) { return BadRequest("Error during delete"); }
            return Ok(DeletedUser);
        }
    }
}
