using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Logging.Loggers;
using GameStore.DataAccess.Entities;

namespace GameStore.Web.Filters
{
    public class LogErrorFilter : IExceptionFilter
    {
        ILogWrapper _logger = DependencyResolver.Current.GetService<ILogWrapper>();
        public void OnException(ExceptionContext filterContext)
        {
            
            _logger.Write(null, filterContext.Exception, filterContext.Exception.Message, LogLevels.Error);
            filterContext.Result = new HttpStatusCodeResult(400);
            filterContext.ExceptionHandled = true;
        }
    }
}