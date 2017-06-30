using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStoreWebApplication.web.Filters;

namespace GameStoreWebApplication.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogErrorFilter());
            filters.Add(new LogIPFilter());
            filters.Add(new ActionPerformanceFilter());
        }
    }
}