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
                name: "busket",
                url: "busket",
                defaults: new { controller = "order", action = "busket" }
            );

            routes.MapRoute(
                name: "publisher",
                url: "publisher/new",
                defaults: new { controller = "publisher", action = "new" }
            );

            routes.MapRoute(
               name: "gameKey",
               url: "game/{key}",
               defaults: new { controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "publisherDetails",
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
                url: "{controller}s/{action}/{id}",
                defaults: new { controller = "Game", action = "GetGames", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}/{gamekey}",
                defaults: new { controller = "Game", action = "GetGames", id = UrlParameter.Optional, gamekey = UrlParameter.Optional }
            );
        }
    }
}
