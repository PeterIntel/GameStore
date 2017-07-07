using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Services_interfaces;
using GameStore.Domain.BusinessObjects;
using System.Web.UI;
using AutoMapper;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        // GET: Comment
        [HttpPost]
        [ActionName("newcomment")]
        public ActionResult AddComment(string gamekey, CommentViewModel commentViewModel)
        {
            var comment = _mapper.Map<CommentViewModel, Comment>(commentViewModel);
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

            throw new ArgumentException("The game was not specified!!!");
        }
    }
}
