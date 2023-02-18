using Data.DTO_s;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Bussines.Abstract;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        => _authService = authService;

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            return Ok(result);
        }

    }
}
