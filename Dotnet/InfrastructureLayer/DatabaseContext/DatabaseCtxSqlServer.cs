using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.DomainLayer.Seeds;
using Microsoft.EntityFrameworkCore;

namespace DotNet6Mediator.InfrastructureLayer.DatabaseContext
{
    public class DatabaseCtxSqlServer : DbContext
    {
        public DatabaseCtxSqlServer(DbContextOptions<DatabaseCtxSqlServer> opts) : base(opts) { }

        public DbSet<User>? UserTable { get; set; }
        public DbSet<Role>? RoleTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //USER CONFIG
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Id).IsRequired().HasColumnName("Id").HasColumnType("integer").ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired().HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<User>().Property(user => user.Surname).IsRequired().HasColumnName("Surname").HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<User>().Property(user => user.BirthDate).IsRequired().HasColumnName("BirthDate").HasColumnType("date");
            modelBuilder.Entity<User>().Property(user => user.Username).IsRequired().HasColumnName("Username").HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<User>().Property(user => user.Password).IsRequired().HasColumnName("Password").HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<User>().Property(user => user.FK_Role).IsRequired().HasColumnName("FK_Role").HasColumnType("integer").HasMaxLength(30);
            //ROLE CONFIG
            modelBuilder.Entity<Role>().HasKey(role => role.Id);
            modelBuilder.Entity<Role>().Property(role => role.Id).IsRequired().HasColumnName("Id").HasColumnType("integer").ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(role => role.RoleName).IsRequired().HasColumnName("RoleName").HasColumnType("varchar").HasMaxLength(30);
            //RELATIONSHIPS CONFIGURATION
            modelBuilder.Entity<Role>().HasMany<User>(role => role.Users).WithOne(user => user.UserRole).HasForeignKey(user => user.FK_Role);
            //DATA SEEDING
            modelBuilder.Entity<Role>().HasData(RoleSeeds.Seeds.ToArray());
            modelBuilder.Entity<User>().HasData(UserSeeds.Seeds.ToArray());
        }
    }
    
}
