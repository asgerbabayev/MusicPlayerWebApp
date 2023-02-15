using AutoMapper;
using Data.DTO_s;
using Data.Entities;

namespace Bussines.Mappings;

public class MappingPofile : Profile
{
	public MappingPofile()
	{
		CreateMap<Song, SongCreateDto>().ReverseMap();
	}
}
