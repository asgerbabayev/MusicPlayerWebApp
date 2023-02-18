using Data.DTO_s;
using Data.Entities;

namespace Bussines.Abstract;

public interface ISongService
{
    Task Add(SongCreateDto song);
    Task<IEnumerable<Song>> GetAll();
}
