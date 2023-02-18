using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.Base;
using DataAccess.DataContext;

namespace DataAccess.Concrete;

public class SongRepository : RepositoryBase<Song, int, AppDbContext>, ISongRepository
{
    public SongRepository(AppDbContext context) : base(context) { }
}