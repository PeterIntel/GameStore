using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IGameService gameService, IMapper mapper)
        {
            _commentService = commentService;
            _gameService = gameService;
            _mapper = mapper;
        }
        // GET: Comment
        [HttpPost]
        [ActionName("newcomment")]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentsViewModel commentsViewModel)
        {
            string gamekey = commentsViewModel.Comment.GameKey;

            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<CommentViewModel, Comment>(commentsViewModel.Comment);
                _commentService.Add(comment);
                commentsViewModel.Comment = new CommentViewModel()
                {
                    GameId = commentsViewModel.Comment.GameId,
                    GameKey = gamekey
                };
                ModelState.Clear();
            }

            commentsViewModel.Comments = InitComments(gamekey).Comments;
            return PartialView("_CommentsPartialView", commentsViewModel);
        }

        [ActionName("comments")]
        [HttpGet]
        public ActionResult GetCommentsForGame(string gameKey)
        {
            if (gameKey != null)
            {
                var game = _gameService.GetItemByKey(gameKey);
                if (game.IsSqlEntity == false)
                {
                    _gameService.Add(game);
                }
                return View(InitComments(gameKey));
            }

            throw new ArgumentException("The game was not specified!!!");
        }

        private CommentsViewModel InitComments(string gameKey)
        {
            var comments = _commentService.GetStructureOfComments(_commentService.GetAllCommentsByGameKey(gameKey));

            var commentsViewModel = new CommentsViewModel()
            {
                Comments = _mapper.Map<IEnumerable<Comment>, IList<CommentViewModel>>(comments),
                Comment = new CommentViewModel()
                {
                    GameId = _gameService.GetItemByKey(gameKey).Id,
                    GameKey = gameKey
                }
            };
            return commentsViewModel;
        }
    }
}
