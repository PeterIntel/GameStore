using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers.Api
{
    public class GamesController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IMapper _mapper;

        public GamesController(IApiAuthentication authentication, IGameService gameService, IGenreService genreService, IPublisherService publisherService,
            IPlatformTypeService platformTypeService, IMapper mapper) : base(authentication)
        {
            _gameService = gameService;
            _genreService = genreService;
            _publisherService = publisherService;
            _platformTypeService = platformTypeService;
            _mapper = mapper;
        }

        public IHttpActionResult GetAllByCompanyName(string key, string contentType)
        {
            if (!_publisherService.Any(x => x.CompanyName == key))
            {
                return Content(HttpStatusCode.BadRequest, "Publisher with such company name does not exist");
            }

            var games = _gameService.Get(x => x.Publisher.CompanyName == key, CurrentLanguage);
            var model = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games);

            return Serialize(model, contentType);
        }

        public IHttpActionResult GetAllByGenreName(string key, string contentType)
        {
            if (!_genreService.Any(x => x.Locals.Any(y => y.Name == key)))
            {
                return Content(HttpStatusCode.BadRequest, "Genre with such name does not exist");
            }

            var games = _gameService.Get(x => x.Genres.Any(y => y.Locals.Any(z => z.Name == key)), CurrentLanguage);
            var model = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games);

            return Serialize(model, contentType);
        }

        public IHttpActionResult Get(string key, string contentType)
        {
            if (!_gameService.Any(x => x.Key == key))
            {
                return Content(HttpStatusCode.BadRequest, "Game with such key does not exist");
            }

            _gameService.AddViewToGame(key, CurrentLanguage);
            var game = _gameService.First(x => x.Key == key, CurrentLanguage);
            var gameViewModel = _mapper.Map<Game, GameViewModel>(game);

            return Serialize(gameViewModel, contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Post(GameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var game = _mapper.Map<GameViewModel, Game>(model);
            _gameService.Add(game, CurrentLanguage);

            return Ok();
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Put(string key)
        {
            if (!_gameService.Any(x => x.Key == key))
            {
                return Content(HttpStatusCode.BadRequest, "Game with such key does not exist");
            }

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var game = _gameService.First(x => x.Key == key, CurrentLanguage);
            _gameService.Update(game, CurrentLanguage);

            return Ok();
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Delete(string key)
        {
            var game = _gameService.First(x => x.Key == key, CurrentLanguage);

            if (!_gameService.Any(x => x.Key == key))
            {
                return Content(HttpStatusCode.BadRequest, "Game with such key does not exist");
            }

            if (ModelState.IsValid)
            {
                _gameService.Remove(game.Id);
            }

            return Ok();
        }

        [ActionName("games")]
        public IHttpActionResult GetGames(string contentType)
        {
            PaginationGames games = _gameService.Get(CurrentLanguage);

            FilterCriteria filter = new FilterCriteria() //TODO Required: remove useless '()'
            {
                Genres = _genreService.Get(CurrentLanguage),
                Platformtypes = _platformTypeService.Get(CurrentLanguage),
                Publishers = _publisherService.Get(CurrentLanguage)
            };

            var filterViewModel = _mapper.Map<FilterCriteria, FilterCriteriaViewModel>(filter);

            var pageInfo = new PagingInfoViewModel() //TODO Required: remove useless '()'
			{
                CurrentPage = 1,
                ItemsPerPage = "10",
                TotalItems = games.Count
            };
	        //TODO Required: Line per Property
			var result = Serialize(new GamesAndFilterViewModel() { Filter = filterViewModel, Games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games.Games), PagingInfo = pageInfo }, contentType); //TODO Required: remove useless '()'

			return result;
        }

        [ActionName("filter")]
        public IHttpActionResult FilterGames(string contentType, FilterCriteriaViewModel filterViewModel, string size, int page = 1)
        {
            PaginationGames games; //TODO Required: Join declaration and assignment

			if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }
            FilterCriteria filters = _mapper.Map<FilterCriteriaViewModel, FilterCriteria>(filterViewModel);
            games = _gameService.FilterGames(filters, page, size, CurrentLanguage);

            filterViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAllGenresAndMarkSelectedForFilter(filterViewModel.NameGenres, CurrentLanguage));
            filterViewModel.PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAllPlatformTypesAndMarkSelected(filterViewModel.NamePlatformTypes, CurrentLanguage));
            filterViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.GetAllPublishersAndMarkSelected(filterViewModel.NamePublishers, CurrentLanguage));

            var pageInfo = new PagingInfoViewModel() //TODO Required: remove useless '()'
			{
                CurrentPage = page,
                ItemsPerPage = size,
                TotalItems = games.Count
            };
	        //TODO Required: Line per Property
			var result = Serialize(new GamesAndFilterViewModel() { Filter = filterViewModel, Games = _mapper.Map<IEnumerable<Game>, IList<GameViewModel>>(games.Games), PagingInfo = pageInfo }, contentType);

            return result;
        }
    }
}
