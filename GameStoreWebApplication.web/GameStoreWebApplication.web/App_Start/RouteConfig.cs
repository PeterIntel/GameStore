using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStoreWebApplication.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "addGame",
                url: "/games/new",
                defaults: new {controller = "Game", action = "AddGame"}
                );

            routes.MapRoute(
                name: "updateGame",
                url: "/games/update",
                defaults: new { controller = "Game", action = "UpdateGame"}
                );

            routes.MapRoute(
               name: "removeGame",
               url: "/games/remove",
               defaults: new { controller = "Game", action = "RemoveGame" }
               );

            routes.MapRoute(
                name: "getGames",
                url: "/games",
                defaults: new { controller = "Game", action = "GetGames"}
                );

            routes.MapRoute(
                name: "addComment",
                url: "/game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "AddComment"}
                );

            routes.MapRoute(
               name: "getCommentsForGame",
               url: "/game/{gamekey}/comments",
               defaults: new { controller = "Commnet", action = "GetCommentsForGame" }
               );

            routes.MapRoute(
                name: "downloadGame",
                url: "/game/{gamekey}/download",
                defaults: new { controller = "Download", action = "DownloadGame" }
                );

            routes.MapRoute(
              name: "getGame",
              url: "/game/{key}",
              defaults: new { controller = "Game", action = "GetGames" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
