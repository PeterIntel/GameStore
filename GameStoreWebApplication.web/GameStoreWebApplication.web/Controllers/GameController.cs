using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainLayer.contracts.DomainModels;
using GameStore.services.Services;
using System.Web.UI;
using NLog;

namespace GameStoreWebApplication.web.Controllers
{
    public class GameController : Controller
    {
        private IGameService _gameService;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        // GET: Game
        [HttpPost]
        [ActionName("new")]
        public ActionResult AddGame(Game game)
        {
            try
            {
                _gameService.Add(game);
                logger.Debug("Sample debug message");
                return new HttpStatusCodeResult(200);
            }
            catch
            {
                logger.Debug("Sample debug message");
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
            int k = 42;
            int l = 100;

            logger.Trace("Sample trace message, k={0}, l={1}", k, l);
            logger.Debug("Sample debug message, k={0}, l={1}", k, l);
            logger.Info("Sample informational message, k={0}, l={1}", k, l);
            logger.Warn("Sample warning message, k={0}, l={1}", k, l);
            logger.Error("Sample error message, k={0}, l={1}", k, l);
            logger.Fatal("Sample fatal error message, k={0}, l={1}", k, l);
            logger.Log(LogLevel.Info, "Sample informational message, k={0}, l={1}", k, l);
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