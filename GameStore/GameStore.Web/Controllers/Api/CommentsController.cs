using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using AutoMapper.XpressionMapper.Extensions;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers.Api
{
    public class CommentsController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(IApiAuthentication authentication, ICommentService commentService, IMapper mapper) : base(authentication)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public IHttpActionResult GetAllByGameKey(string key, string contentType)
        {
            if (!_commentService.Any(x => x.Game.Key == key))
            {
                return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments"); //TODO Consider: Return 200Ok with empty comments array
			}

            var comments = _commentService.GetAllCommentsByGameKey(key);

            var model = _mapper.Map<IEnumerable<Comment>, IList<CommentViewModel>>(comments);

            return Serialize(model, contentType);
        }

        public IHttpActionResult Get(string key, string id, string contentType) //TODO Required: remove 'key'
		{
            if (!_commentService.Any(x => x.Game.Key == key)) //TODO Required: remove
            {
                return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments"); //TODO Consider: Return 200Ok with empty comments array
			}

            if (!_commentService.Any(x => x.Id == id))
            {
                return Content(HttpStatusCode.BadRequest, "Comment with such id does not exist");
            }

            var comment = _commentService.GetItemById(id);

            var model = _mapper.Map<Comment, CommentViewModel>(comment);

            return Serialize(model, contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Forbid, RoleEnum.Moderator, RoleEnum.Administrator, RoleEnum.Manager)]
        public IHttpActionResult Post(CommentViewModel commentViewModel)
        {
            if (!_commentService.Any(x => x.Game.Key == commentViewModel.GameKey)) //TODO Required: remove
			{
                return Content(HttpStatusCode.BadRequest, "This game does not have comments");
            }

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var comment = _mapper.Map<CommentViewModel, Comment>(commentViewModel);
            _commentService.Add(comment, CurrentLanguage);

            return Ok();
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Moderator)]
        public IHttpActionResult Put(CommentViewModel model)
        {
            if (!_commentService.Any(x => x.Game.Key == model.GameKey))
            {
                return Content(HttpStatusCode.BadRequest, "This game does not have comments"); //TODO Required: remove
			}

            if (!_commentService.Any(x => x.Id == model.Id))
            {
                return Content(HttpStatusCode.BadRequest, "Comment with such id does not exist");
            }

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var comment = _mapper.Map<CommentViewModel, Comment>(model);
            _commentService.Update(comment, CurrentLanguage);

            return Ok();
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Moderator)]
        public IHttpActionResult Delete(string key, string id) //TODO Required: remove 'key'
		{
            if (!_commentService.Any(x => x.Game.Key == key))
            {
                return Content(HttpStatusCode.BadRequest, "This does not have comments"); //TODO Required: remove
			}

            if (!_commentService.Any(x => x.Id == id))
            {
                return Content(HttpStatusCode.BadRequest, "Comment with such id does not exist");
            }

            _commentService.Remove(id);

            return Ok();
        }
    }
}
