using DotNet6Mediator.ApplicationLayer.Attributes;

namespace DotNet6Mediator.ApplicationLayer.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        private static readonly string _message = "Unauthorized request";

        public ForbiddenAccessException(string? failure) : base(_message)
        {
            this.Data["AuthorizationFailure"] = failure;
        }
    }
}
