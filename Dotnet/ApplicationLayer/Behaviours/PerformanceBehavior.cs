using MediatR;
using System.Diagnostics;

namespace DotNet6Mediator.ApplicationLayer.Behaviours
{
    public class PerformanceBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        private const int MSEC = 100;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavior(ILogger<TRequest> Logger) {
            this._logger = Logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var timer = new Stopwatch();
            timer.Start();
            TResponse Response = await next();
            timer.Stop();
            long Milliseconds = timer.ElapsedMilliseconds;

            string RequestTypeName = typeof(TRequest).Name;
            if (Milliseconds > MSEC) {
                this._logger.LogWarning("\n\tCompleting a request '{RequestTypeName}' in {Milliseconds} milliseconds\n",RequestTypeName,Milliseconds);
            } else {
                this._logger.LogInformation("\n\tCompleting a request '{RequestTypeName}' in {Milliseconds} milliseconds\n",RequestTypeName, Milliseconds);
            }
            return Response;
        }
    }
}
