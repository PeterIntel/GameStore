using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [CustomAuthorize(RoleEnum.Manager)]
    public class GenreController : Controller
    {

        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        // GET: Genre
        public ActionResult GetGenres()
        {
            return View(_mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(_genreService.Get()));
        }

        [ActionName("new")]
        public ActionResult AddGenre()
        {
            var genres = _genreService.Get();

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
                _genreService.Add(genre);

                return RedirectToAction("GetGenres");
            }

            genreViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get());

            return View(genreViewModel);
        }

        public ActionResult Edit(string key)
        {
            Genre genre = _genreService.First(x => x.Name == key);
            if (genre == null)
            {
                return HttpNotFound();
            }

            var genreViewModel = _mapper.Map<Genre, GenreViewModel>(genre);
            genreViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get(g => g.Id != genreViewModel.Id));
            genreViewModel.Genres.Insert(0, new GenreViewModel() { Name = "Not Specified"});

            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<GenreViewModel, Genre>(genreViewModel);
                _genreService.Update(genre);

                return RedirectToAction("getGenres");
            }

            genreViewModel.Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.Get(g => g.Id != genreViewModel.Id));
            genreViewModel.Genres.Insert(0, new GenreViewModel() { Name = "Not Specified" });

            return View(genreViewModel);
        }

        public ActionResult Delete(string key)
        {
            var genre = _genreService.First(x => x.Name == key);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<Genre, GenreViewModel>(genre));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDelete(string id)
        {
            var genre = _genreService.First(x => x.Id == id);
            if (ModelState.IsValid)
            {
                _genreService.Remove(id);

                return RedirectToAction("getGenres");
            }

            return View(_mapper.Map<Genre, GenreViewModel>(genre));
        }
    }
}