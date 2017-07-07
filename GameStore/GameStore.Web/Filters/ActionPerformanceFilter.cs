using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Logging.Loggers;

namespace GameStore.Web.Filters
{
    public class ActionPerformanceFilter : IActionFilter
    {
        ILogWrapper _logger = DependencyResolver.Current.GetService<ILogWrapper>();
        DateTime _start;
        DateTime _end;
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _start = DateTime.UtcNow;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _end = DateTime.UtcNow;
            _logger.Write("performanceLog", null, "Performance time: " + new TimeSpan((_end - _start).Milliseconds), LogLevels.Info);
        }
    }
}