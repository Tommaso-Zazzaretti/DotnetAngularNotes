namespace DotNet6Mediator.InfrastructureLayer.Services.Interfaces
{
    public interface IAuthenticationService<TResource,TCredentials> where TResource : class 
                                                                    where TCredentials : class
    {
        public Task<TResource?> Authenticate(TCredentials AuthCredentials);
    }
}
