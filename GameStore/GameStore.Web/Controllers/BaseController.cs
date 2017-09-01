using System.Web.Mvc;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IAuthentication Auth { set; get; }
        public BaseController(IAuthentication auth)
        {
            Auth = auth;
        }

        protected User CurrentUser
        {
            get { return ((IUserProvider)Auth.CurrentUser.Identity).User; }
        }
    }
}