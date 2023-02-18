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
    => await _context.Set<TEntity>().AddAsync(entity);
    public async Task<TEntity?> Find(TKey id)
       => await _context.Set<TEntity>().FindAsync(id);
    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null)
    => expression == null
            ? await _context.Set<TEntity>().ToListAsync()
            : await _context.Set<TEntity>().Where(expression).ToListAsync();



    public async Task<IEnumerable<TEntity?>> GetAllWithInclude(params string[] includes)
    {
        return await Include(includes).AsQueryable().ToListAsync();
    }


    private IQueryable<TEntity> Include(params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
        if (includes != null)
            foreach (string item in includes)
                query = query.Include(item);
        return query;
    }
    public async Task Remove(TEntity entity)
    => _context.Set<TEntity>().Remove(entity);

    public async Task Update(TEntity entity)
    => _context.Set<TEntity>().Update(entity);
}
