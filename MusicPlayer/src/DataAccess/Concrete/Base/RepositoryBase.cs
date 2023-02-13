using Data.Entities.Base;
using DataAccess.Abstract.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.Base;

public class RepositoryBase<TEntity, TKey, TContext> : IRepositoryBase<TEntity, TKey>
    where TContext : DbContext
    where TEntity : BaseEntity
{
    private readonly TContext _context;

    public RepositoryBase(TContext context) =>
        _context = context;

    public async Task Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> Find(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null)
    {
        return expression == null
            ? await _context.Set<TEntity>().ToListAsync()
            : await _context.Set<TEntity>().Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllWithInclude(Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await Include(includeProperties).ToListAsync();
    }

    private IQueryable<TEntity> Include(Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        return includeProperties.Aggregate(query, (current, includeProperty) =>
        current.Include(includeProperty));
    }

    public async Task Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
