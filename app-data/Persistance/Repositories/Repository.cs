using app_domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace app_data.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return true;
        }

        public async Task<bool> Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return true;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return true;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}
