using Data.Entities.Base;
using System.Linq.Expressions;

namespace DataAccess.Abstract.Base;

public interface IRepositoryBase<TEntity, TKey>
    where TEntity : BaseEntity
{
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null);
    Task<IEnumerable<TEntity>> GetAllWithInclude(Expression<Func<TEntity, object>>[] includeProperties);
    Task Remove(TEntity entity);
    Task<TEntity> Find(TKey id);
}
