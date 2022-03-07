namespace DotNet6Mediator.InfrastructureLayer.Exceptions
{
    public class PersistenceRuleException : Exception
    {
        private static readonly string _message = "An error occurred during the database transaction";
        public PersistenceRuleException(IEnumerable<String> ErrorsList) : base(_message) {
            this.Data["PersistenceFailures"] = ErrorsList.ToList();
        }
    }
}
