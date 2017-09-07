using System.Web.Mvc;
using GameStore.Logging.Loggers;

namespace GameStore.Web.Filters
{
    public class LogIpFilter : ActionFilterAttribute
    {
        private readonly ILogWrapper _logger = DependencyResolver.Current.GetService<ILogWrapper>();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Write(null, null, "User IP Address: " + filterContext.HttpContext.Request.UserHostAddress, LogLevels.Info);
        }
    }
}