using System.Linq.Expressions;


namespace app_domain.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
