using app_domain.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace app_data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Todo> Todos { get; set; }
    }
}
