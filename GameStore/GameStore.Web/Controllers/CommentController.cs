using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.DataAccess.Entities;
using GameStore.Domain.ServicesInterfaces;
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
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentsViewModel commentsViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<CommentViewModel, Comment>(commentsViewModel.Comment);
                _commentService.Add(comment);
                ModelState.Clear();
                commentsViewModel.Comment = new CommentViewModel()
                {
                    GameKey = commentsViewModel.Comment.GameKey
                };
            }
            commentsViewModel.Comments = InitComments(commentsViewModel.Comment.GameKey).Comments;
            return PartialView("_CommentsPartialView", commentsViewModel);
        }

        [ActionName("comments")]
        [HttpGet]
        public ActionResult GetCommentsForGame(string gameKey)
        {
            if (gameKey != null)
            {
                return View(InitComments(gameKey));
            }

            throw new ArgumentException("The game was not specified!!!");
        }c

        private CommentsViewModel InitComments(string gameKey)
        {
            var comments = _commentService.GetStructureOfComments(_commentService.GetAllCommentsByGameKey(gameKey));

            var commentsViewModel = new CommentsViewModel()
            {
                Comments = _mapper.Map<IEnumerable<Comment>, IList<CommentViewModel>>(comments),
                Comment = new CommentViewModel() { GameKey = gameKey}
            };
            return commentsViewModel;
        }
    }
}
