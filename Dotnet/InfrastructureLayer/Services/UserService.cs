using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.InfrastructureLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using DotNet6Mediator.InfrastructureLayer.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNet6Mediator.InfrastructureLayer.Services
{
    public class UserService : ICrudService<User>
    {
        private readonly DatabaseCtxSqlServer _dbContext;

        public UserService(DatabaseCtxSqlServer Ctx)
        {
            this._dbContext = Ctx;
        }

        public async Task<User?> Create(User NewEntity)
        {
            //Check if Username is not already used...
            IQueryable<User?> GetUserByUsername = this._dbContext.UserTable!.Where(user => user.Username == NewEntity.Username);
            User? UserByUsername = await GetUserByUsername.FirstOrDefaultAsync();
            if (UserByUsername != null) {
                throw new PersistenceRuleException(new List<string>() { "Username '" + NewEntity.Username + "' already taken" });
            }
            //Check if Role exist..
            IQueryable<Role?> GetRoleById = this._dbContext.RoleTable!.Where(role => role.RoleName == NewEntity.UserRole!.RoleName);
            Role? FkRole = await GetRoleById.FirstOrDefaultAsync();
            if (FkRole == null) {
                throw new PersistenceRuleException(new List<string>() { "Role '" + NewEntity.UserRole!.RoleName + "' does not exist" });
            }
            
            NewEntity.FK_Role = FkRole.Id;
            NewEntity.Id = default;  //UNSET PK => SERIAL FIELD
            //Add the new User
            EntityEntry<User> trackingEntity = await this._dbContext.UserTable!.AddAsync(NewEntity);
            await this._dbContext.SaveChangesAsync();
            return trackingEntity.Entity;
        }
        public async Task<User?> Retrieve(int Id)
        {
            IQueryable<User?> GetUserById = this._dbContext.UserTable!.Include(user => user.UserRole).Where(user => user.Id == Id);
            User? UserById = await GetUserById.FirstOrDefaultAsync();
            if (UserById == null) {
                throw new PersistenceRuleException(new List<string>() { "There is no user with id '" + Id + "'" });
            }
            return UserById;
        }
        public async Task<User?> Update(User UpdateEntity)
        {
            //Check if User exist..
            IQueryable<User?> GetUserByUsername = this._dbContext.UserTable!.Where(user => user.Username == UpdateEntity.Username);
            User? UserByUsername = await GetUserByUsername.FirstOrDefaultAsync();
            if (UserByUsername == null) {
                throw new PersistenceRuleException(new List<string>() { "There is no user with username '" + UpdateEntity.Username + "' to update" });
            }
            //Check if Role exist..
            IQueryable<Role?> GetRoleByName = this._dbContext.RoleTable!.Where(role => role.RoleName == UpdateEntity.UserRole!.RoleName);
            Role? FkRole = await GetRoleByName.FirstOrDefaultAsync();
            if (FkRole == null) {
                throw new PersistenceRuleException(new List<string>() { "Role '" + UpdateEntity.UserRole!.RoleName + "' does not exist" });
            }
            //Update
            UserByUsername.Name = UpdateEntity.Name;
            UserByUsername.Surname = UpdateEntity.Surname;
            UserByUsername.BirthDate = UpdateEntity.BirthDate;
            UserByUsername.Password = UpdateEntity.Password;
            UserByUsername.FK_Role = FkRole.Id;
            UserByUsername.UserRole = FkRole;
            await this._dbContext.SaveChangesAsync();
            return UserByUsername;
        }
        public async Task<User?> Delete(int Id)
        {
            //Check if User exist
            IQueryable<User?> GetUserById = this._dbContext.UserTable!.Include(user => user.UserRole).Where(user => user.Id == Id);
            User? UserById = await GetUserById.FirstOrDefaultAsync();
            if (UserById == null) {
                throw new PersistenceRuleException(new List<string>() { "There is no user with id '" + Id + "' to Delete" });
            }
            //Remove
            var trackingEntity = this._dbContext.UserTable!.Remove(UserById);
            await this._dbContext.SaveChangesAsync();
            return trackingEntity.Entity;
        }
    }
}
