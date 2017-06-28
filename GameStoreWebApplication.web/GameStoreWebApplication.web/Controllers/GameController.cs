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
        private IGameService _gameService;

        public GameController()
        {
            _gameService = new GameService();
        }
        // GET: Game
        [HttpPost]
        [ActionName("new")]
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
        [ActionName("update")]
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

        [ActionName("remove")]
        [HttpPost]
        public ActionResult RemoveGame(Game game)
        {
            try
            {
                _gameService.Remove(game);
                return new HttpStatusCodeResult(200);
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetGames(string key)
        {

            try
            {
                if (key == null)
                {
                    return Json(_gameService.GetAll(null), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(_gameService.GetItemByKey(key), JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }
    }
}