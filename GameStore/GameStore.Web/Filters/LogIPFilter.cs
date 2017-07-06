using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Logging.Loggers;

namespace GameStore.Web.Filters
{
    public class LogIPFilter : IActionFilter
    {
        ILogWrapper _logger = DependencyResolver.Current.GetService<ILogWrapper>();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Write(null, null, "User IP Address: " + filterContext.HttpContext.Request.UserHostAddress, LogLevels.Info);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}