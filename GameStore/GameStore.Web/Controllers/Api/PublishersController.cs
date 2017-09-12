using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers.Api
{
    public class PublishersController : BaseController
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublishersController(IApiAuthentication authentication, IPublisherService publisherService, IMapper mapper) : base(authentication)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        public IHttpActionResult GetAllByGameKey(string key, string contentType)
        {
            if (!_publisherService.Any(x => x.Games.Any(y => y.Key == key)))
            {
                return Content(HttpStatusCode.OK, "Game with such key does not have publishers");
            }

            var publishers = _publisherService.Get(x => x.Games.Any(y => y.Key == key), CurrentLanguage);

            var model = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(publishers);

            return Serialize(model, contentType);
        }

        [ActionName("publishers")]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult GetPublishers(string contentType)
        {
            return Serialize(_mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherViewModel>>(_publisherService.Get(CurrentLanguage)), contentType);
        }

        [HttpPost]
        public IHttpActionResult Post(PublisherViewModel publisherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = _mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);
            _publisherService.Add(publisher, CurrentLanguage);

            return Ok();
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Get(string key, string contentType)
        {
            var publisher = _publisherService.First(x => x.CompanyName == key, CurrentLanguage); //TODO Consider: use 'var' here

			if (publisher == null)
            {
                return Content(HttpStatusCode.NotFound, CreateError());
            }

            var publisherViewModel = _mapper.Map<Publisher, PublisherViewModel>(publisher);

            return Serialize(publisherViewModel, contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Publisher)]
        public IHttpActionResult GetCurrentPublisher(string contentType)
        {
            if (CurrentUser.User.Publisher != null)
            {
                var publisher = _publisherService.First(x => x.CompanyName == CurrentUser.User.Publisher.CompanyName, CurrentLanguage); //TODO Consider: use 'var' here

				if (publisher == null)
                {
                    return Content(HttpStatusCode.NotFound, CreateError());
                }

                var publisherViewModel = _mapper.Map<Publisher, PublisherViewModel>(publisher);

                return Serialize(publisherViewModel, contentType);
            }

            return Content(HttpStatusCode.Forbidden, CreateError());
        }

        [HttpPost]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Put(PublisherViewModel publisherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = _mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);
            _publisherService.Update(publisher, CurrentLanguage);

            return Ok();
        }

        [HttpPost]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Delete(string key)
        {
            var publisher = _publisherService.First(x => x.CompanyName == key, CurrentLanguage);

            if (publisher == null)
            {
                return Content(HttpStatusCode.NotFound, CreateError());
            }

            _publisherService.Remove(publisher.Id);

            return Ok();
        }
    }
}
