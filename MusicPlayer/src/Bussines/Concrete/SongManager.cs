using AutoMapper;
using Bussines.Abstract;
using Data.DTO_s;
using Data.Entities;
using DataAccess.Abstract.UnitOfWork;

namespace Bussines.Concrete;

public class SongManager : ISongService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public SongManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Add(SongCreateDto songDto)
    {
        var result = _mapper.Map<Song>(songDto);
        await _unitOfWork.SongRepository.Add(result);
        await _unitOfWork.SaveChangesAsync();
    }
}
