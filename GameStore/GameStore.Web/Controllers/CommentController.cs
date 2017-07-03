using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Services_interfaces;
using GameStore.Domain.Business_objects;
using System.Web.UI;

namespace GameStore.Web.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        // GET: Comment
        [HttpPost]
        [ActionName("newcomment")]
        public ActionResult AddComment(string gamekey, Comment comment)
        {
            comment.Game.Key = gamekey;
            _commentService.Add(comment);
            return new HttpStatusCodeResult(200);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        [ActionName("comments")]
        public ActionResult GetCommentsForGame(string gameKey)
        {
            if (gameKey != null)
            {
                return Json(_commentService.GetAllCommentsByGameKey(gameKey), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("None comment has the game!");
            }
        }
    }
}
