using MediatR;
using System.Reflection;
using DotNet6Mediator.ApplicationLayer.Attributes;
using DotNet6Mediator.ApplicationLayer.Exceptions;
using System.Security.Claims;
using DotNet6Mediator.ApplicationLayer.Services.Interfaces;

namespace DotNet6Mediator.ApplicationLayer.Behaviours
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TRequest> _logger;
        private readonly IAuthorizationRoleService _authorizationRoleService;


        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ILogger<TRequest> Logger, IAuthorizationRoleService AuthrService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._logger = Logger;
            this._authorizationRoleService = AuthrService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Verifico se la richiesta ha degli attributi di autorizzazione
            IEnumerable<AuthorizationRequirementAttribute> Attributes  = request.GetType().GetCustomAttributes<AuthorizationRequirementAttribute>();
            //Se ne ha...
            if (Attributes.Any())
            {                
                //Recupero lo UserID dall'identity della richiesta (precedentemente settata dall'autentication Behavior)
                string? UserId = this._httpContextAccessor.HttpContext?.User?.Claims.Where(e => e.Type == "UserID").FirstOrDefault()?.Value;
                if(UserId == null) {
                    throw new ForbiddenAccessException("User ID is null");
                }
                try   { int.Parse(UserId); } 
                catch { throw new ForbiddenAccessException("Invalid User ID"); }
                
                //Isolo tutti gli attributi aventi il parametro roles, ad esempio: [AuthorizationRequirement(Roles="Admin,User")]
                IEnumerable<AuthorizationRequirementAttribute> RolesFilteredAttributes = Attributes.Where(attr => !string.IsNullOrWhiteSpace(attr.Roles));
                //Se ce ne sono..
                if (RolesFilteredAttributes.Any())
                {
                    bool IS_AUTHORIZED = false;
                    foreach (var Attribute in RolesFilteredAttributes)
                    {
                        IEnumerable<string> Roles = Attribute.Roles.Split(",");                        
                        bool UserHasRole = await this._authorizationRoleService.IsAuthorized(int.Parse(UserId),Roles);
                        if (UserHasRole) { 
                            IS_AUTHORIZED = true;
                            break; 
                        }
                    }
                    // Must be a member of at least one role in roles
                    if (!IS_AUTHORIZED) {
                        throw new ForbiddenAccessException("User with ID '"+UserId+"' does not have the required permission role");
                    }
                }
            }
            this._logger.LogInformation("\n\tAuthorized Request \n");
            return await next();
        }
    }
}
