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
    public class CommentController : Controller
    {
        private CommentService _commentService;
        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            try
            {
                _commentService.Add(comment);
                return new HttpStatusCodeResult(200); 

            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetCommentsForGame(string gameKey)
        {
            try
            {
                if (gameKey != null)
                {
                    return Json(_commentService.GetAllCommentsByGameKey(gameKey));
                }
                else
                {
                    return Content("None comment has the game!");
                }
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }
    }
}