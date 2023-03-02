using Data.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Bussines.Abstract;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthApiController(IAuthService authService)
        => _authService = authService;

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if (!result.data.Succeeded)
            {
                foreach (var error in result.data.Errors)
                    return BadRequest(registerDto);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
