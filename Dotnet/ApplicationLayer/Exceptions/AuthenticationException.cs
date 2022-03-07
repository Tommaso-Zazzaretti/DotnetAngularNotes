namespace DotNet6Mediator.ApplicationLayer.Exceptions
{
    public class AuthenticationException : Exception
    {
        private static readonly string _message = "Unauthenticated request";

        public AuthenticationException(string? failure) : base(_message)
        {
            //Converto i failures in una lista di stringhe di errori
            this.Data["AuthenticationFailure"] = failure;
        }
    }
}
