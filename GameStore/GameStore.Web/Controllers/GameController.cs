using System.Web.Mvc;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IPublisherService publisherService, IMapper mapper)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("new")]
        public ActionResult AddGame()
        {
            var game = new GameViewModel()
            {
                Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAll()),
                PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAll()),
                Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.GetAll())
            };
            return View(game);
        }

        [HttpPost]
        [ActionName("new")]
        public ActionResult AddGame(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var game = _mapper.Map<GameViewModel, Game>(gameViewModel);
                _gameService.Add(game);
                return RedirectToAction("GetGames");
            }
            gameViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelected(gameViewModel.NameGenres));
            gameViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(gameViewModel.NamePlatformtypes));
            gameViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.GetAll());
            return View(gameViewModel);
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

        public ActionResult GetGames()
        {
            FilterCriteria filter = new FilterCriteria()
            {
                Genres = _genreService.GetAll(),
                Platformtypes = _platformTypeService.GetAll(),
                Publishers = _publisherService.GetAll()
            };

            var filterViewModel = _mapper.Map<FilterCriteria, FilterCriteriaViewModel>(filter);

            return View(new GamesAndFilterViewModel() { Filter = filterViewModel, Games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(_gameService.GetAll())});
        }
        
        [ActionName("filter")]
        public ActionResult FilterGames(FilterCriteriaViewModel filter)
        {
            return View();
        }

        public ActionResult GetGameDetails(string key)
        {
            var gameViewModel = _mapper.Map<Game, GameViewModel>(_gameService.GetItemByKey(key));
            return View(gameViewModel);
        }

        [ActionName("download")]
        public ActionResult DownloadPage(string gamekey)
        {
            return View((object)gamekey);
        }

        public FileResult DownloadGame(string gamekey)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/download/download.exe"));
            string filename = "download.exe";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}