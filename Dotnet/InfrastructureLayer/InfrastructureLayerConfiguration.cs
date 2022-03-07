using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.DomainLayer.Helpers;
using DotNet6Mediator.InfrastructureLayer.DatabaseContext;
using DotNet6Mediator.InfrastructureLayer.Services;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNet6Mediator.InfrastructureLayer
{
    public static class InfrastructureLayerConfiguration
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DatabaseCtxSqlServer>(opts => opts.UseSqlServer(Configuration["ConnectionString"]));
            services.AddScoped<ICrudService<User>, UserService>();
            services.AddScoped<IAuthenticationService<User,UserCredentials>,UserAuthenticationService>();
            return services;
        }
    }
}
