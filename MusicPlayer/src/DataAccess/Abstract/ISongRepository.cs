using Data.Entities;
using DataAccess.Abstract.Base;

namespace DataAccess.Abstract;

public interface ISongRepository : IRepositoryBase<Song, int>
{
}
