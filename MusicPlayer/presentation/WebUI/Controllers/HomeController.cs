using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;
using Bussines.Abstract;
using Data.DTO_s;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService _service;

        public HomeController(ILogger<HomeController> logger, ISongService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index(SongCreateDto song)
        {
            await _service.Add(song);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}