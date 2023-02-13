using Bussines.Abstract;
using Data.DTO_s;
using Data.Entities;
using DataAccess.Abstract;

namespace Bussines.Concrete;

public class SongManager : ISongService
{
    private readonly ISongRepository _songRepository;

    public SongManager(ISongRepository songRepository) =>
        _songRepository = songRepository;

    public async Task Add(SongCreateDto song)
    {
        await _songRepository.Add(new Song
        {
            Name = song.Name,
            ArtistId = song.ArtistId
        });
    }
}
