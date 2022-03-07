namespace DotNet6Mediator.ApplicationLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizationRequirementAttribute : Attribute
    {
        public AuthorizationRequirementAttribute() { }

        public string Roles { get; set; } = string.Empty;
    }
}
