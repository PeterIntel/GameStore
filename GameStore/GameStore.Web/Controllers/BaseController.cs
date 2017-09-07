using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.Controllers
{
    public class BaseController : Controller
    {
        private string _currentLanguageCode; //TODO Required: put to top
        protected IAuthentication Auth { set; get; }
        protected string CurrentLanguageCode { set; get; }
    
        public BaseController(IAuthentication auth)
        {
            Auth = auth;
        }

        protected User CurrentUser
        {
            get { return ((IUserProvider)Auth.CurrentUser.Identity).User; }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            CurrentLanguageCode = (string)requestContext.RouteData.Values["lang"];
            if (requestContext.RouteData.Values["lang"] != null &&
                requestContext.RouteData.Values["lang"] is string)
            {
                _currentLanguageCode = requestContext.RouteData.Values["lang"] as string;
                try
                {
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture =
                        new CultureInfo(_currentLanguageCode);
                }
                catch (Exception) //TODO Required: remove useless 'ex'
				{
                    throw new NotSupportedException($"Invalid language code '{_currentLanguageCode}'.");
                }
            }

            base.Initialize(requestContext);
        }
    }
}