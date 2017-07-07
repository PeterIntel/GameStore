using System.Web.Mvc;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.Services_interfaces;
using System.Web.UI;
using GameStore.Web.ViewModels;
using AutoMapper;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }
        // GET: Game
        [HttpGet]
        [ActionName("new")]
        public ActionResult AddGame(GameViewModel gameViewModel)
        {
            var game = _mapper.Map<GameViewModel, Game>(gameViewModel);
            //_gameService.Add(game);
            return View();
        }
        [ActionName("update")]
        [HttpPost]
        public ActionResult UpdateGame(GameViewModel gameViewModel)
        {
            var game = _mapper.Map<GameViewModel, Game>(gameViewModel);
            _gameService.Update(game);
            return new HttpStatusCodeResult(200);
        }

        [ActionName("remove")]
        [HttpPost]
        public ActionResult RemoveGame(GameViewModel gameViewModel)
        {
            var game = _mapper.Map<GameViewModel, Game>(gameViewModel);
            _gameService.Remove(game);
            return new HttpStatusCodeResult(200);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetGames()
        {
            return Json(_gameService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGameDetails(string key)
        {
            return Json(_gameService.GetItemByKey(key), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public FileResult DownloadGame(int? gamekey)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/download/download.exe"));
            string filename = "download.exe";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}