using app_domain.Core.Models;
using app_domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace app_data.Persistance.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(DbContext context) : base(context)
        {
        }
        public DbContext GetContext
        {
            get { return Context; }
        }
    }
}
