using DotNet6Mediator.ApplicationLayer.Services.Interfaces;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.InfrastructureLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DotNet6Mediator.ApplicationLayer.Services
{
    public class AuthorizationService : IAuthorizationRoleService
    {
        private readonly DatabaseCtxSqlServer _dbContext;

        public AuthorizationService(DatabaseCtxSqlServer Context)
        {
            this._dbContext = Context;
        }

        public async Task<bool> IsAuthorized(int UserId, IEnumerable<string> Roles)
        {
            IQueryable<User> GetUserById = this._dbContext.UserTable!.Include(user => user.UserRole).Where(user => user.Id == UserId);
            User? UserById = await GetUserById.FirstOrDefaultAsync();
            if (UserById == null) { return false; }
            if (UserById.UserRole == null) { return false; }
            return Roles.Select(e => e.Trim()).Contains(UserById.UserRole.RoleName.Trim());
        }
    }
}
