using System;
using System.Web.Mvc;
using GameStore.Logging.Loggers;

namespace GameStore.Web.Filters
{
    public class ActionPerformanceFilter : IActionFilter
    {
        private readonly ILogWrapper _logger = DependencyResolver.Current.GetService<ILogWrapper>();
        private DateTime _start;
        private DateTime _end;
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