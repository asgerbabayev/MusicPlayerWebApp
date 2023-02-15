namespace DataAccess.Abstract.UnitOfWork;

public interface IUnitOfWork
{
    ISongRepository? SongRepository { get; set; }
    Task<int> SaveChangesAsync();
}
