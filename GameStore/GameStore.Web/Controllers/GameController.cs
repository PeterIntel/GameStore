using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Business_objects;
using GameStore.Domain.Services_interfaces;
using System.Web.UI;
using NLog;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        // GET: Game
        [HttpPost]
        [ActionName("new")]
        public ActionResult AddGame(Game game)
        {
            _gameService.Add(game);
            return new HttpStatusCodeResult(200);
        }
        [ActionName("update")]
        [HttpPost]
        public ActionResult UpdateGame(Game game)
        {
            throw new Exception(this.GetType().Name);
            _gameService.Update(game);
            return new HttpStatusCodeResult(200);
        }

        [ActionName("remove")]
        [HttpPost]
        public ActionResult RemoveGame(Game game)
        {
            _gameService.Remove(game);
            return new HttpStatusCodeResult(200);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetGames(string key)

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
    }
}