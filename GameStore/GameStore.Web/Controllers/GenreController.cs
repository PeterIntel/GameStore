using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.Controllers
{
    [CustomAuthorize(RoleEnum.Manager)]
    public class GenreController : BaseController
    {

        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper, IAuthentication auth) : base(auth)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        public ActionResult GetGenres()
        {
            return View(_mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(_genreService.Get(CurrentLanguageCode)));
        }

        [ActionName("new")]
        public ActionResult AddGenre()
        {
            var genres = _genreService.Get(CurrentLanguageCode);

            return View(new GenreViewModel() { Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(genres)});
        }

        [ActionName("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGenre(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<GenreViewModel, Genre>(genreViewModel);
                _genreService.Add(genre, CurrentLanguageCode);

                return RedirectToAction("GetGenres");
            }

            genreViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get(CurrentLanguageCode));

            return View(genreViewModel);
        }

        public ActionResult Edit(string key)
        {
            Genre genre = _genreService.GetFirstGenreByName(key, CurrentLanguageCode);
            if (genre == null)
            {
                return HttpNotFound();
            }

            var genreViewModel = _mapper.Map<Genre, GenreViewModel>(genre);
            genreViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get(g => g.Id != genreViewModel.Id, CurrentLanguageCode));
            if (genre.ParentGenre != null)
            {
                genreViewModel.Genres.Insert(0, new GenreViewModel() {Name = Resources.NotSpecified});
            }

            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<GenreViewModel, Genre>(genreViewModel);
                _genreService.Update(genre, CurrentLanguageCode);

                return RedirectToAction("getGenres");
            }

            genreViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get(g => g.Id != genreViewModel.Id, CurrentLanguageCode));
            if (genreViewModel.ParentGenreId != null)
            {
                genreViewModel.Genres.Insert(0, new GenreViewModel() { Name = Resources.NotSpecified });
            }

            return View(genreViewModel);
        }

        public ActionResult Delete(string key)
        {
            var genre = _genreService.GetFirstGenreByName(key, CurrentLanguageCode);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<Genre, GenreViewModel>(genre));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string id)
        {
            var genre = _genreService.First(x => x.Id == id, CurrentLanguageCode);
            if (ModelState.IsValid)
            {
                _genreService.Remove(id);

                return RedirectToAction("getGenres");
            }

            return View(_mapper.Map<Genre, GenreViewModel>(genre));
        }
    }
}