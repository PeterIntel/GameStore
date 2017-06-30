using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logging;

namespace GameStoreWebApplication.web.Filters
{
    public class LogIPFilter : IActionFilter
    {
        IWrapper _logger = DependencyResolver.Current.GetService<WrapNLogLogger>();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Write(null, "User IP Address: " + filterContext.HttpContext.Request.UserHostAddress, LogLevels.Info);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}