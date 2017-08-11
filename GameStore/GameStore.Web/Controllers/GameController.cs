using System;
using System.Web.Mvc;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using GameStore.Web.Filters;

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
                Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get()),
                PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.Get()),
                Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get())
            };
            return View(game);
        }

        [HttpPost]
        [ActionName("new")]
        [AddGameErrorFilter(ExceptionType = typeof(SqlException))]
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
            gameViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get());
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
            PaginationGames games = _gameService.Get();

            FilterCriteria filter = new FilterCriteria()
            {
                Genres = _genreService.Get(),
                Platformtypes = _platformTypeService.Get(),
                Publishers = _publisherService.Get()
            };

            var filterViewModel = _mapper.Map<FilterCriteria, FilterCriteriaViewModel>(filter);

            var pageInfo = new PagingInfoViewModel()
            {
                CurrentPage = 1,
                ItemsPerPage = "10",
                TotalItems = games.Count
            };

            TempData["games"] = games;
            return View(new GamesAndFilterViewModel() { Filter = filterViewModel, Games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games.Games), PagingInfo = pageInfo });
        }

        [ActionName("filter")]
        public ActionResult FilterGames(FilterCriteriaViewModel filterViewModel, string size, int page = 1)
        {
            PaginationGames games;

            if (ModelState.IsValid)
            {
                FilterCriteria filters = _mapper.Map<FilterCriteriaViewModel, FilterCriteria>(filterViewModel);
                games = _gameService.FilterGames(filters, page, size);
                TempData["games"] = games;
            }
            else
            {
                games = TempData["games"] as PaginationGames;
                TempData["games"] = games;
            }

            filterViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelected(filterViewModel.NameGenres));
            filterViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(filterViewModel.NamePlatformTypes));
            filterViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.GetAllPublishersAndMarkSelected(filterViewModel.NamePublishers));

            var pageInfo = new PagingInfoViewModel()
            {
                CurrentPage = page,
                ItemsPerPage = size,
                TotalItems = games.Count
            };

            return View("GetGames", new GamesAndFilterViewModel() { Filter = filterViewModel, Games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games.Games), PagingInfo = pageInfo });
        }

        public ActionResult GetGameDetails(string key)
        {
            _gameService.AddViewToGame(key);
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