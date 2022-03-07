namespace DotNet6Mediator.ApplicationLayer.Services.Interfaces
{
    public interface IAuthorizationRoleService
    {
        public Task<bool> IsAuthorized(int UserId, IEnumerable<string> Roles);
    }
}
