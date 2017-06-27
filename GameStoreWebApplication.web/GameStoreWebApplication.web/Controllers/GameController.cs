using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainLayer.contracts.DomainModels;
using GameStore.services.Services;
using System.Web.UI;

namespace GameStoreWebApplication.web.Controllers
{
    public class GameController : Controller
    {
        private GameService _gameService;
        // GET: Game
        [HttpPost]
        public ActionResult AddGame(Game game)
        {
            try
            {
                _gameService.Add(game);
                return new HttpStatusCodeResult(200);
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }

        [HttpPost]
        public ActionResult UpdateGame(Game game)
        {
            try
            {
                _gameService.Update(game);
                return new HttpStatusCodeResult(200);
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }

        [HttpPost]
        public ActionResult RemoveGame(int gameId)
        {
            try
            {
                _gameService.Remove(gameId);
                return new HttpStatusCodeResult(200);
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetGames(int? gameId)
        {
            try
            {
                if (gameId == null)
                {
                    return Json(_gameService.GetAll(null));
                }
                else
                {
                    return Json(_gameService.GetItemById((int)gameId));
                }
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }
    }
}