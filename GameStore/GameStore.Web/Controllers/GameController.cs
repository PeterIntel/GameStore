﻿using System;
using System.Web.Mvc;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using System.Web.UI;
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
        private readonly IMapper _mapper;
        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IMapper mapper)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("new")]
        public ActionResult AddGame()
        {
            var game = new GameViewModel()
            {
                Genres = _mapper.Map<IEnumerable<Genre>, IList<GenreViewModel>>(_genreService.GetAll()),
                PlatformTypes = _mapper.Map<IEnumerable<PlatformType>, IList<PlatformTypeViewModel>>(_platformTypeService.GetAll())
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
                game.Genres = _mapper.Map<IEnumerable<GenreViewModel>, IEnumerable<Genre>>(gameViewModel.Genres.Where(x => x.IsChecked));
                game.PlatformTypes = _mapper.Map<IEnumerable<PlatformTypeViewModel>, IEnumerable<PlatformType>>(gameViewModel.PlatformTypes.Where(x => x.IsChecked));
                _gameService.Add(game);
                return RedirectToAction("GetGames");
            }
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

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetGames()
        {
            return Json(_gameService.GetAll().ToList(), JsonRequestBehavior.AllowGet);
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

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public FileResult DownloadGame(string gamekey)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/download/download.exe"));
            string filename = "download.exe";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}