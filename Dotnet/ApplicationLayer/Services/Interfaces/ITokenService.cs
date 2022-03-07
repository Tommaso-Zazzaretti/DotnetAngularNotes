using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.DomainLayer.Helpers;
using System.Security.Claims;

namespace DotNet6Mediator.ApplicationLayer.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string?> GetToken(UserCredentials UserCredentials);
        public IEnumerable<Claim> ValidateToken(string TokenString);


    }
}
