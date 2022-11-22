using app_domain.Core.Repositories;
using app_domain.Models;
using Microsoft.EntityFrameworkCore;

namespace app_data.Persistance.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public DbContext GetContext
        {
            get { return Context; }
        }
    }
}
