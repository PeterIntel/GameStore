using System;
using System.Web.Caching;
using System.Web.Mvc;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Web.Filters
{
    public class GetNumberOfGamesFilter : ActionFilterAttribute
    {
        private IGameService _gameService = DependencyResolver.Current.GetService<IGameService>();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cache = filterContext.HttpContext.Cache;
            if (cache["GamesQuantity"] == null)
            {
                cache["GamesQuantity"] = _gameService.Get().Count;
                cache.Add("GamesQuantity", cache["GamesQuantity"], null, DateTime.UtcNow.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            base.OnActionExecuted(filterContext);
        }
    }
}