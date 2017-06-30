using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logging;

namespace GameStoreWebApplication.web.Filters
{
    public class ActionPerformanceFilter : IActionFilter
    {
        IWrapper _logger = DependencyResolver.Current.GetService<WrapNLogLogger>();
        DateTime _start;
        DateTime _end;
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _start = DateTime.Now;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _end = DateTime.Now;
            //_logger.Write(null, "Performance time: " + new TimeSpan(_end - _), LogLevels.Info)
        }
    }
}