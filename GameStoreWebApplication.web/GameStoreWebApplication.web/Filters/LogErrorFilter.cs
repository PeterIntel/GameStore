using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logging;
using GameDataAccessLayer.DAL.Repositories;
using DomainLayer.contracts.DomainModels;

namespace GameStoreWebApplication.web.Filters
{
    public class LogErrorFilter : IExceptionFilter
    {
        IGenericDataRepository<Game> g = DependencyResolver.Current.GetService <IGenericDataRepository<Game>>();
        IWrapper _logger = DependencyResolver.Current.GetService<WrapNLogLogger>();
        public void OnException(ExceptionContext filterContext)
        {
            
            _logger.Write(filterContext.Exception, filterContext.Exception.Message, LogLevels.Error);
            filterContext.Result = new HttpStatusCodeResult(400);
            filterContext.ExceptionHandled = true;
        }
    }
}