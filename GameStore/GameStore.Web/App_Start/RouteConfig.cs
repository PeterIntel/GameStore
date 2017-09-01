using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "busket",
                url: "busket",
                defaults: new { controller = "order", action = "busket" }
            );

            routes.MapRoute(
                name: "games",
                url: "games",
                defaults: new { controller = "Game", action = "games" }
            );

            routes.MapRoute(
                name: "orders",
                url: "orders",
                defaults: new { controller = "Order", action = "getCurrentOrders" }
            );

            routes.MapRoute(
               name: "gameKey",
               url: "game/{key}",
               defaults: new { controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "publisherDetails",
                url: "publisher/{action}/{CompanyName}",
                defaults: new { controller = "publisher", CompanyName = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "gameKeyAction",
                 url: "game/{gamekey}/{action}",
                 defaults: new {controller = "game"}
             );

            routes.MapRoute(
                name: "defaultS",
                url: "{controller}s/{action}/{key}",
                defaults: new { controller = "Game", action = "GetGames", key = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}/{gamekey}",
                defaults: new { controller = "Game", action = "GetGames", id = UrlParameter.Optional, gamekey = UrlParameter.Optional }
            );
        }
    }
}
