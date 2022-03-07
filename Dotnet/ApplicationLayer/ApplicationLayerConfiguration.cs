using DotNet6Mediator.ApplicationLayer.Behaviours;
using DotNet6Mediator.ApplicationLayer.Services;
using DotNet6Mediator.ApplicationLayer.Services.Interfaces;
using DotNet6Mediator.DomainLayer.Helpers;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace DotNet6Mediator.ApplicationLayer
{
    public static class ApplicationLayerConfiguration
    {
        //Extension method: possible call: services.AddApplicationLayer in Startup.cs
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationRoleService  , AuthorizationService>();
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthenticationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddControllers();
            services.AddSwaggerGen();
            return services;
        }

        public static void UseApplicationLayer(this WebApplication app)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (app.Environment.IsDevelopment()) {
                //Swagger
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
