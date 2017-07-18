using System;
using System.Collections.Generic;
using System.Configuration;
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
               defaults: new { controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "CompanyName",
                url: "publisher/{CompanyName}",
                defaults: new { controller = "publisher", action = "GetPublisherDetails", CompanyName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "gameKeyComment",
                url: "game/{gamekey}/comments",
                defaults: new { controller = "comment", action = "comments" }
            );

            routes.MapRoute(
                 name: "gameKeyAction",
                 url: "game/{gamekey}/{action}",
                 defaults: new {controller = "game"}
             );

            routes.MapRoute(
                name: "defaultS",
                url: "{controller}s/{action}",
                defaults: new { controller = "Game", action = "GetGames" }
                );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}/{gamekey}",
                defaults: new { controller = "Game", action = "GetGames", id = UrlParameter.Optional, gamekey = UrlParameter.Optional }
            );
        }
    }
}
