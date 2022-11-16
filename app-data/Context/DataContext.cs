using app_domain.Models;
using Microsoft.EntityFrameworkCore;


namespace app_data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}
