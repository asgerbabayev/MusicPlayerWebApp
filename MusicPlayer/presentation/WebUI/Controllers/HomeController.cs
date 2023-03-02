using Bussines.Abstract;
using Data.DTO_s;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Bussines.Abstract;
using WebUI.ActionFilters;
namespace WebUI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAuthService _authService;
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService _service;

        public HomeController(ILogger<HomeController> logger,
            ISongService service,
            IAuthService authService)
        {
            _logger = logger;
            _service = service;
            _authService = authService;
        }

        //[Authorize]
        [MyAuthorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {

            return View(await _service.GetAll());
        }

        public async Task<IActionResult> Privacy()
        {
            await _authService.CreateRole();
            return View();
        }

        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            return Ok(result);
        }
    }
}