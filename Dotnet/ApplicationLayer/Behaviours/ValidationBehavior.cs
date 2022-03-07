using DotNet6Mediator.ApplicationLayer.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Behaviours
{
    /*
        La mia classe ValidationBehaviour è un behaviour in quanto implementa 
        l'interfaccia IPipelineBehaviour.

        L'interfaccia IPipelineBehaviour deve essere associata ogni generica coppia <TRequest,TResponse> 
        trovata nell'assembly configurato, tale che ogni TRequest sia una classe che implementa 
        l'iterfaccia IRequest<TResponse> di MediatR (ovvero tale che sia un Command o una Query).

                IPipelineBehavior<TRequest, TResponse>    where    TRequest : IRequest<TResponse>

                                  Tutte le coppie<A,B>  tali che   A sia una IRequest di MediatR
    */
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        //Questa variabile contiene tutti e solo i validatori associati a un tipo TRequest presenti nell'assembly attuale
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> Validators)  
        {
            _validators = Validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Se in questo assembly esistono classi che implementano l'interfaccia IValidator associate a un tipo TRequest...
            if (_validators.Any()) 
            {
                /* Un oggetto ValidationContext<T> è associato a una richiesta di tipo T, ed è
                   in grado di dedurre qual è l'istanza da validare contenuta nel tipo T.
                   Serve ad astrarre l'istanza da validare, in quanto in questa classe non ho a 
                   disposizione il tipo definito della richiesta, ma solo un tipo generico da cui
                   non è possibile capire cosa c'è dentro
                */
                var Context = new ValidationContext<TRequest>(request);
                //Validazione Asincrona di tutti i validatori associati a una richiesta di tipo TRequest
                ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(Context, cancellationToken)));
                //Listing degli errori riscontrati
                List<ValidationFailure> Failures = validationResults.SelectMany(rule => rule.Errors).Where(f => f != null).ToList();
                if (Failures.Count != 0) {
                    throw new InvalidatedRequest(Failures);
                } 
            }
            return await next();
        }
    }
    
}
