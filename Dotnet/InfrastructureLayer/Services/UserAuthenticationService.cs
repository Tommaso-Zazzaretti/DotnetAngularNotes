using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.DomainLayer.Helpers;
using DotNet6Mediator.InfrastructureLayer.DatabaseContext;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNet6Mediator.InfrastructureLayer.Services
{
    public class UserAuthenticationService : IAuthenticationService<User,UserCredentials>
    {
        private readonly DatabaseCtxSqlServer _dbContext;

        public UserAuthenticationService(DatabaseCtxSqlServer Service)
        {
            this._dbContext = Service;
        }
        
        public async Task<User?> Authenticate(UserCredentials Credentials)
        {
            //Definisco una query da eseguire in differita
            IQueryable<User> UserById = this._dbContext.UserTable!.Include(user => user.UserRole).Where(user => (user.Username == Credentials.Username && user.Password == Credentials.Password));
            //Eseguo la query e prendo il primo risultato
            User? AuthUser = await UserById.FirstOrDefaultAsync();
            return AuthUser;
        }
    }
}
