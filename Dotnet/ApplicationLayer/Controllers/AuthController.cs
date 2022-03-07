using DotNet6Mediator.DomainLayer.Helpers;
using DotNet6Mediator.ApplicationLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6Mediator.ApplicationLayer.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _service;

        public AuthController(ITokenService Service)
        {
            this._service = Service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials? UserCredentials)
        {
            if (UserCredentials == null) { return BadRequest("Body is null"); }
            string? TokenString = await this._service.GetToken(UserCredentials);
            if (TokenString == null) { return BadRequest("Error during create"); }
            return Ok(new TokenResponse(){ Token = TokenString });
        }
    }
}
