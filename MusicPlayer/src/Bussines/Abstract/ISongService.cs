using Data.DTO_s;

namespace Bussines.Abstract;

public interface ISongService
{
    Task Add(SongCreateDto song);
}
