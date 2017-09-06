using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
    public class RouteConfig
    {
        private const string LangConstraint = @"en|ru";
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region DefaultRoutes
            routes.MapRoute(
                name: "busket",
                url: "busket",
                defaults: new { lang = "en", controller = "order", action = "busket" }
            );

            routes.MapRoute(
                name: "games",
                url: "games",
                defaults: new { lang = "en", controller = "Game", action = "games" }
            );

            routes.MapRoute(
                name: "orders",
                url: "orders",
                defaults: new { lang = "en", controller = "Order", action = "getCurrentOrders" }
            );

            routes.MapRoute(
               name: "gameKey",
               url: "game/{key}",
               defaults: new { lang = "en", controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "publisherDetails",
                url: "publisher/{action}/{CompanyName}",
                defaults: new { lang = "en", controller = "publisher", CompanyName = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "gameKeyAction",
                 url: "game/{gamekey}/{action}",
                 defaults: new { lang = "en", controller = "game" }
             );

            routes.MapRoute(
                name: "defaultS",
                url: "{controller}s/{action}/{key}",
                defaults: new { lang = "en", controller = "Game", action = "Games", key = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}/{gamekey}",
                defaults: new { lang = "en", controller = "Game", action = "Games", id = UrlParameter.Optional, gamekey = UrlParameter.Optional }
            );
            #endregion

            #region LocalizedRoutes
            routes.MapRoute(
                name: "Lbusket",
                url: "{lang}/busket",
                defaults: new { controller = "order", action = "busket" },
                constraints: new { LangConstraint}
            );

            routes.MapRoute(
                name: "Lgames",
                url: "{lang}/games",
                defaults: new { lang = "en", controller = "Game", action = "games" },
                constraints: new { LangConstraint }
            );

            routes.MapRoute(
                name: "Lorders",
                url: "{lang}/orders",
                defaults: new { lang = "en", controller = "Order", action = "getCurrentOrders" },
                constraints: new { LangConstraint }
            );

            routes.MapRoute(
                name: "LgameKey",
                url: "{lang}/game/{key}",
                defaults: new { lang = "en", controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional },
                constraints: new { LangConstraint }
            );

            routes.MapRoute(
                name: "LpublisherDetails",
                url: "{lang}/publisher/{action}/{CompanyName}",
                defaults: new { lang = "en", controller = "publisher", CompanyName = UrlParameter.Optional },
                constraints: new { LangConstraint }
            );

            routes.MapRoute(
                name: "LgameKeyAction",
                url: "{lang}/game/{gamekey}/{action}",
                defaults: new { lang = "en", controller = "game" },
                constraints: new { LangConstraint }
            );

            routes.MapRoute(
                name: "LdefaultS",
                url: "{lang}/{controller}s/{action}/{key}",
                defaults: new { lang = "en", controller = "Game", action = "Games", key = UrlParameter.Optional },
                constraints: new { LangConstraint }
            );

            routes.MapRoute(
                name: "Ldefault",
                url: "{lang}/{controller}/{action}/{id}/{gamekey}",
                defaults: new { lang = "en", controller = "Game", action = "Games", id = UrlParameter.Optional, gamekey = UrlParameter.Optional },
                constraints: new { LangConstraint }
            );
            #endregion
        }
    }
}
