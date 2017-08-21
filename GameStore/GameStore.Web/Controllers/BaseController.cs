using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Authorization;
using GameStore.Domain.BusinessObjects;
using Ninject;

namespace GameStore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IAuthentication Auth { set; get; }

        protected User CurrentUser
        {
            get { return ((IUserProvider)Auth.CurrentUser.Identity).User; }
        }
        public BaseController(IAuthentication auth)
        {
            Auth = auth;
        }
    }
}