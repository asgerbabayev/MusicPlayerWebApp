using DataAccess.Abstract;
using DataAccess.Abstract.UnitOfWork;
using DataAccess.DataContext;

namespace DataAccess.Concrete.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public ISongRepository? SongRepository { get; set; }
    private AppDbContext context;
    public UnitOfWork(ISongRepository songRepository, AppDbContext context)
    {
        SongRepository = songRepository;
        this.context = context;
    }

    public async Task<int> SaveChangesAsync()
    => await context.SaveChangesAsync();

}
