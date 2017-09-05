using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IGameService gameService, IMapper mapper, IAuthentication auth) : base(auth)
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
                _commentService.Add(comment, CurrentLanguageCode);
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
                var game = _gameService.GetItemByKey(gameKey, CurrentLanguageCode);
                // If user come with direct link to game's comments then copy game to the sql databse if necessary
                if (game.IsSqlEntity == false)
                {
                    _gameService.Add(game, CurrentLanguageCode);
                }

                return View(InitComments(gameKey));
            }

            throw new ArgumentException("The game was not specified!!!");
        }

        private CommentsViewModel InitComments(string gameKey)
        {
            var comments = _commentService.GetStructureOfComments(_commentService.GetAllCommentsByGameKey(gameKey));
            var game = _gameService.GetItemByKey(gameKey, CurrentLanguageCode);

            var commentsViewModel = new CommentsViewModel()
            {
                Comments = _mapper.Map<IEnumerable<Comment>, IList<CommentViewModel>>(comments),
                Comment = new CommentViewModel()
                {
                    GameId = game.Id,
                    IsDeletedGame = game.IsDeleted,
                    GameKey = gameKey
                }
            };

            return commentsViewModel;
        }

        [HttpGet]
        [CustomAuthorize(RoleEnum.Moderator)]
        public ActionResult ChangeCommentState(string key)
        {
            var comment = _commentService.First(x => x.Id == key, CurrentLanguageCode);
            comment.IsDisabled = !comment.IsDisabled;
            _commentService.Update(comment, CurrentLanguageCode);

            return RedirectToAction("comments", new {gameKey = comment.Game.Key});
        }
    }
}
