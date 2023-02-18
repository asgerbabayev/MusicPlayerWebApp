namespace DataAccess.Abstract.UnitOfWork;

public interface IUnitOfWork
{
    ISongRepository? SongRepository { get; }
    Task<int> SaveChangesAsync();
}
