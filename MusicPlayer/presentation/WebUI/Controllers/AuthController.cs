using Data.DTO_s;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Bussines.Abstract;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        => _authService = authService;


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return View(registerDto);
            var result = await _authService.Register(registerDto);
            if (!result.data.Succeeded)
            {
                foreach (var error in result.data.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(registerDto);
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);
            var result = await _authService.Login(loginDto);
            if (!result.data.Succeeded)
            {
                foreach (var error in result.data.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(loginDto);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
