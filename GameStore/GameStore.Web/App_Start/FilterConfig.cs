using System.Web.Mvc;
using GameStore.Web.Filters;

namespace GameStore.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogErrorFilter());
            filters.Add(new LogIpFilter());
            filters.Add(new ActionPerformanceFilter());
            filters.Add(new GetNumberOfGamesFilter());
        }
    }
}