using app_domain.Core.Repositories;


namespace app_domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        ITodoRepository Todos { get; }
        Task<int> CompleteAsync();
    }
}
