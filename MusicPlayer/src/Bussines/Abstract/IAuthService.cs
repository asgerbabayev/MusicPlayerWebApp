using Data.DTO_s;
using MusicPlayer.Bussines.Results;

namespace MusicPlayer.Bussines.Abstract;

public interface IAuthService
{
    Task<Response> Login(LoginDto loginDto);
    Task<Response> Register(RegisterDto registerDto);
    Task CreateRole();
}
