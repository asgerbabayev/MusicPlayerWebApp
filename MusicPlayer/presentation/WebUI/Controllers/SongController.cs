using Bussines.Abstract;
using Data.DTO_s;
using DataAccess.Abstract.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SongController : Controller
    {
        private readonly ISongService _songService;
        private readonly IUnitOfWork unitOfWork;

        public SongController(ISongService songService, IUnitOfWork unitOfWork)
        {
            _songService = songService;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Artists = await unitOfWork.SongRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SongCreateDto songCreateDto)
        {
            await _songService.Add(songCreateDto);
            return RedirectToAction("Index");
        }
    }
}
