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
            int countFilterGames;

            FilterCriteria filter = new FilterCriteria()
            {
                Genres = _genreService.Get(),
                Platformtypes = _platformTypeService.Get(),
                Publishers = _publisherService.Get()
            };

            var games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(_gameService.Get(out countFilterGames));

            var filterViewModel = _mapper.Map<FilterCriteria, FilterCriteriaViewModel>(filter);

            var pageInfo = new PagingInfoViewModel()
            {
                CurrentPage = 1,
                ItemsPerPage = 10,
                TotalItems = countFilterGames
            };

            return View(new GamesAndFilterViewModel() { Filter = filterViewModel, Games = games, PagingInfo = pageInfo });
        }
        
        [ActionName("filter")]
        public ActionResult FilterGames(FilterCriteriaViewModel filterViewModel, string size, int page = 1)
        {
            int countFilterGames;
            IList<GameViewModel> games;
            int? maxSize = size != "ALL" ? (int?)int.Parse(size) : null;

            if (ModelState.IsValid)
            {
                // TODO: This is BLL responsability. Method FilterGames should be Get overload method with filter paramenters.
                FilterCriteria filters = _mapper.Map<FilterCriteriaViewModel, FilterCriteria>(filterViewModel);
                if (maxSize != null)
                {
                    games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(_gameService.FilterGames(filters, out countFilterGames, page, (int) maxSize));
                }
                else
                {
                    games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(_gameService.Get(out countFilterGames));
                }
            }
            else
            {
                games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(_gameService.Get(out countFilterGames));
            }

            filterViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelected(filterViewModel.NameGenres));
            filterViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(filterViewModel.NamePlatformTypes));
            filterViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.GetAllPublishersAndMarkSelected(filterViewModel.NamePublishers));

            var pageInfo = new PagingInfoViewModel()
            {
                CurrentPage = page,
                ItemsPerPage = maxSize,
                TotalItems = countFilterGames
            };

            return View("GetGames", new GamesAndFilterViewModel() { Filter = filterViewModel, Games = games,PagingInfo = pageInfo});
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