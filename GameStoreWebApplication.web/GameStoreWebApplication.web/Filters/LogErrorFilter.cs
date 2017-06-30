using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logging;
using Ninject;

namespace GameStoreWebApplication.web.Filters
{
    public class LogErrorFilter : IExceptionFilter
    {
        IWrapper _logger = DependencyResolver.Current.GetService<WrapNLogLogger>();
        public void OnException(ExceptionContext filterContext)
        {
            
            _logger.Write(filterContext.Exception, filterContext.Exception.Message, LogLevels.Error);
            filterContext.Result = new HttpStatusCodeResult(400);
            filterContext.ExceptionHandled = true;
        }
    }
}