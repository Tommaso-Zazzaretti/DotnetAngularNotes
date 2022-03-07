using FluentValidation.Results;

namespace DotNet6Mediator.ApplicationLayer.Exceptions
{
    public class InvalidatedRequest : Exception
    {
        private static readonly string _message = "An error occurred while validating the request input data";

        public InvalidatedRequest(IEnumerable<ValidationFailure> Failures) : base(_message)
        {
            //Converto i failures in una lista di stringhe di errori
            this.Data["ValidationFailures"] = Failures.Select(elem => elem.ErrorMessage).ToList();
        }        
    }
}
