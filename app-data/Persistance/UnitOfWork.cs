using app_data.Context;
using app_data.Persistance.Repositories;
using app_domain.Core;
using app_domain.Core.Repositories;

namespace app_data.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Posts = new PostRepository(_context);
            Todos = new TodoRepository(_context);
        }

        public IPostRepository Posts { get; private set; }
        public ITodoRepository Todos { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
