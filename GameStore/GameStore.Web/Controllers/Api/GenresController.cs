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
    [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
    public class GenresController : BaseController
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IApiAuthentication authentication, IGenreService genreService, IMapper mapper) : base(authentication)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        public IHttpActionResult GetAllByGameKey(string key, string contentType)
        {
            if (!_genreService.Any(x => x.Games.Any(y => y.Key == key)))
            {
                return Content(HttpStatusCode.BadRequest, "Game with such key does not have genres");
            }

            var genres = _genreService.Get(x => x.Games.Any(y => y.Key == key), CurrentLanguage);

            var model = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(genres);

            return Serialize(model, contentType);
        }

        public IHttpActionResult GetGenres(string contentType)
        {
            return Serialize(_mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(_genreService.Get(CurrentLanguage)), contentType);
        }

        [HttpPost]
        public IHttpActionResult Post(GenreViewModel genreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var genre = _mapper.Map<GenreViewModel, Genre>(genreViewModel);
            _genreService.Add(genre, CurrentLanguage);

            return Ok();
        }

        public IHttpActionResult Put(GenreViewModel genreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var genre = _mapper.Map<GenreViewModel, Genre>(genreViewModel);
            _genreService.Update(genre, CurrentLanguage);

            return Ok();
        }

        public IHttpActionResult Delete(string key)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var genre = _genreService.First(x => x.Locals.Any(z => z.Name == key), CurrentLanguage);
            _genreService.Remove(genre.Id);

            return Ok();
        }
    }
}
