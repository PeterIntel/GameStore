using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
               name: "gameKey",
               url: "game/{key}",
               defaults: new { controller = "Game", action = "GetGames", key = UrlParameter.Optional }
               );

            routes.MapRoute(
             name: "addComment",
             url: "game/{gamekey}/{action}",
             defaults: new { controller = "Comment", action = "AddComment", gamekey = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "defaultS",
                url: "{controller}s/{action}",
                defaults: new { controller = "Game", action = "GetGames" }
                );
            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}",
                defaults: new { controller = "Game", action = "GetGames" }
                );
        }
    }
}
