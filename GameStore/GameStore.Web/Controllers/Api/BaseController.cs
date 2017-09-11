using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Results;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers.Api
{
    public class BaseController : ApiController
    {
        private const string DefaultLanguage = "en";
        private const string XmlContentType = "xml";
        private const string JsonContentType = "json";
        private const string JsonMediaType = "application/json";
        private const string XmlMediaType = "application/xml";

        private readonly IApiAuthentication _authentication;

        public BaseController(IApiAuthentication authentication)
        {
            _authentication = authentication;
        }

        public string CurrentLanguage
        {
            get
            {
                var currentLanguage = ControllerContext.RouteData.Values["lang"]?.ToString() ?? DefaultLanguage;

                return currentLanguage;
            }
        }

        public IUserProvider CurrentUser => (IUserProvider)_authentication.User;

        [HttpPost]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var token = _authentication.Login(model.UserName, model.Password, model.RememberMe);

            if (token == null)
            {
                return Content(HttpStatusCode.BadRequest, "There is no such user");
            }

            return Ok(token);
        }

        protected FormattedContentResult<T> Serialize<T>(T content, string contentType)
        {
            FormattedContentResult<T> result = null;

            if (contentType == JsonContentType)
            {
                result = new FormattedContentResult<T>(
                    HttpStatusCode.OK,
                    content,
                    new JsonMediaTypeFormatter(), 
                    new MediaTypeHeaderValue(JsonMediaType), 
                    this
                    );
            }

            if (contentType == XmlContentType)
            {
                result = new FormattedContentResult<T>(
                    HttpStatusCode.OK,
                    content,
                    new XmlMediaTypeFormatter(), 
                    new MediaTypeHeaderValue(XmlMediaType),
                    this);
            }

            return result;
        }

        protected ErrorViewModel CreateError()
        {
            var error = new ErrorViewModel()
            {
                Message = "ModelState contains errors",
                Errors = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
            };

            return error;
        }
    }
}