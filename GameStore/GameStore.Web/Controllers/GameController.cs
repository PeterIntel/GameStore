using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IPublisherService publisherService, IMapper mapper, IAuthentication authentication) : base(authentication)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("new")]
        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult AddGame()
        {
            var game = new GameViewModel()
            {
                Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get(g => g.Name != "Other")),
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

                return RedirectToAction("games");
            }

            gameViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelected(gameViewModel.NameGenres));
            gameViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(gameViewModel.NamePlatformtypes));
            gameViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get());

            return View(gameViewModel);
        }

        [ActionName("edit")]
        public ActionResult UpdateGame(string gameKey)
        {

            Game game = _gameService.First(g => g.Key == gameKey);
            if (CurrentUser.IsInRole(RoleEnum.Publisher) && (game.Publisher != null && CurrentUser.Publisher != null && CurrentUser.Publisher.CompanyName == game.Publisher.CompanyName) || CurrentUser.IsInRole(RoleEnum.Manager))
            {
                if (game == null || game.IsDeleted)
                {
                    return HttpNotFound();
                }

                var gameViewModel = _mapper.Map<Game, GameViewModel>(game);
                gameViewModel.Genres =
                    _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(
                        _genreService.GetAllGenresAndMarkSelected(gameViewModel.Genres.Select(g => g.Name)));
                gameViewModel.PlatformTypes =
                    _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(
                        _platformTypeService.GetAllPlatformTypesAndMarkSelected(
                            gameViewModel.PlatformTypes.Select(p => p.TypeName)));
                gameViewModel.Publishers =
                    _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get());
                gameViewModel.Publishers.Insert(0, new PublisherViewModel() { CompanyName = "Not Specified" });

                return View(gameViewModel);
            }

            return new HttpStatusCodeResult(403);
        }

        [ActionName("edit")]
        [HttpPost]
        public ActionResult UpdateGame(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var game = _mapper.Map<GameViewModel, Game>(gameViewModel);
                _gameService.Update(game);

                return RedirectToAction("games");
            }

            gameViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelected(gameViewModel.NameGenres));
            gameViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(gameViewModel.NamePlatformtypes));
            gameViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get());
            gameViewModel.Publishers.Insert(0, new PublisherViewModel() { CompanyName = "Not Specified" });

            return View(gameViewModel);
        }

        [ActionName("delete")]
        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult Remove(string gameKey)
        {
            var game = _gameService.First(x => x.Key == gameKey);
            if (game == null || game.IsDeleted)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<Game, GameViewModel>(game));
        }

        [ActionName("delete")]
        [HttpPost]
        public ActionResult RemoveGame(string id)
        {
            var game = _gameService.First(x => x.Id == id);
            if (ModelState.IsValid)
            {
                _gameService.Remove(id);

                return RedirectToAction("games");
            }

            return View(_mapper.Map<Game, GameViewModel>(game));
        }

        [ActionName("games")]
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

            filterViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelectedForFilter(filterViewModel.NameGenres));
            filterViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(filterViewModel.NamePlatformTypes));
            filterViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.GetAllPublishersAndMarkSelected(filterViewModel.NamePublishers));

            var pageInfo = new PagingInfoViewModel()
            {
                CurrentPage = page,
                ItemsPerPage = size,
                TotalItems = games.Count
            };

            return View("games", new GamesAndFilterViewModel() { Filter = filterViewModel, Games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games.Games), PagingInfo = pageInfo });
        }

        public ActionResult GetGameDetails(string key)
        {
            _gameService.AddViewToGame(key);
            var gameViewModel = _mapper.Map<Game, GameViewModel>(_gameService.GetItemByKey(key));
            ViewBag.CurrentUser = CurrentUser;

            return View(gameViewModel);
        }

        [ActionName("download")]
        [CustomAuthorize(RoleEnum.User)]
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