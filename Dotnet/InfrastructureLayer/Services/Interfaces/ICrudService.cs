namespace DotNet6Mediator.InfrastructureLayer.Services.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        public Task<T?> Create(T NewEntity);
        public Task<T?> Retrieve(int Id);
        public Task<T?> Update(T UpdateEntity);
        public Task<T?> Delete(int Id);
    }
}
