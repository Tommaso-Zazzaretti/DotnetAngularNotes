using MediatR;
using System.Reflection;
using DotNet6Mediator.ApplicationLayer.Attributes;
using DotNet6Mediator.ApplicationLayer.Exceptions;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using DotNet6Mediator.ApplicationLayer.Services.Interfaces;

namespace DotNet6Mediator.ApplicationLayer.Behaviours
{
    public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private readonly ILogger<TRequest> _logger;

        public AuthenticationBehavior(IHttpContextAccessor httpContextAccessor, ITokenService Service, ILogger<TRequest> Logger)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._tokenService = Service;
            this._logger = Logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Verifico se la richiesta ha degli attributi di autorizzazione
            IEnumerable<AuthorizationRequirementAttribute> Attributes = request.GetType().GetCustomAttributes<AuthorizationRequirementAttribute>();
            //Se ne ha...
            if (Attributes.Any())
            {
                //Accedo all'header Authorization contenente token/credenziali etc etc...
                HttpRequest Request = _httpContextAccessor.HttpContext!.Request!;
                if (!Request.Headers.ContainsKey(HeaderNames.Authorization)) //Se non esiste un header Authorization
                {
                    throw new AuthenticationException("There is no authentication header in the http request");
                }
                //Accedo al token: il valore dell'header http è : Bearer Kr£dk3k4m...39jr304)$d  => quindi splitto per spazio e prendo l'ultima parte
                string HeaderString = Request.Headers[HeaderNames.Authorization].ToString();
                string TokenString  = HeaderString.Split(" ").Last();
                //Controllo se il token è valido. Se lo è recupero l'id dell'utente contenuto nel token
                IEnumerable<Claim> Claims;
                try {
                    Claims = this._tokenService.ValidateToken(TokenString);
                    int UserId = int.Parse(Claims.First(x => x.Type == "UserID").Value);
                } 
                catch {
                    throw new AuthenticationException("Invalid Token");
                }
                //Autentico la richiesta assegnando al campo HttpContext.User una Identity contenente il suo ID                
                var UserIdentity = new ClaimsIdentity(Claims); 
                this._httpContextAccessor.HttpContext.User = new ClaimsPrincipal(UserIdentity);                
                this._logger.LogInformation("\n\tAuthenticated Request\n");
            }
            return await next();
        }
    }
}
