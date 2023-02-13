using Data.Entities;
using DataAccess.Concrete.Base;
using DataAccess.DataContext;

namespace DataAccess.Concrete;

public class SongRepository : RepositoryBase<Song, int, AppDbContext>
{
    public SongRepository(AppDbContext context) : base(context)
    {
    }
}