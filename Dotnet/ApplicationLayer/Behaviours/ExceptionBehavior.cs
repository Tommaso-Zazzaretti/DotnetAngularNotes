using MediatR;
using DotNet6Mediator.InfrastructureLayer.Exceptions;
using System.Net;
using System.Text.Json;
using DotNet6Mediator.ApplicationLayer.Exceptions;

namespace DotNet6Mediator.ApplicationLayer.Behaviours
{
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TRequest> _logger;

        public ExceptionBehavior(IHttpContextAccessor httpContextAccessor,ILogger<TRequest> Logger)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._logger = Logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try {
                return await next();
            }
            catch (Exception Ex) {
                //Reference to HttpContext Object via HttpContextAccessor Service
                HttpResponse Response = this._httpContextAccessor.HttpContext!.Response;
                Response.Clear();
                Response.ContentType = "application/json";
                //Set the Status Code
                Object? Errors = null;
                switch (Ex)
                {
                    // Custom Authentication Exception: AppLayer/Exception/AuthenticationException.cs
                    case AuthenticationException:
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Errors = Ex.Data["AuthenticationFailure"];
                        break;
                    // Custom Forbidden Exception: AppLayer/Exception/ForbiddenAccessException.cs
                    case ForbiddenAccessException:
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Errors = Ex.Data["AuthorizationFailure"];
                        break;
                    // Custom Validation Exception: AppLayer/Exception/InvalidDataException.cs
                    case InvalidatedRequest: 
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Errors = Ex.Data["ValidationFailures"];
                        break;
                    // Custom Db Ctx Exception: Infrastructure/Exception/PersistenceRuleException.cs
                    case PersistenceRuleException:
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        Errors = Ex.Data["PersistenceFailures"];
                        break;
                    // unhandled error
                    default:
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                //Build the Json Exception Response Body
                string CommandType = typeof(TRequest).Name; //Reflection
                string ExceptionType = Ex.GetType().Name;
                string JsonBody = JsonSerializer.Serialize(new { type = ExceptionType,
                                                                 message = Ex.Message,
                                                                 timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                                                                 failedCmd = CommandType,
                                                                 errors = Errors
                                                               });
                await Response.WriteAsync(JsonBody);

                //Log the raised exception
                _logger.LogError("Exception raised during the execution of request '{CommandType}':\n\n\tType: {ExceptionType}\n\tMessage: {Ex.Message}\n",CommandType,ExceptionType,Ex.Message);
                throw;
            }
        }
    }
}
