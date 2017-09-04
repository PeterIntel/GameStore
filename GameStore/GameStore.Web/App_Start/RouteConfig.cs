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

            #region LocalizedRoutes
            routes.MapRoute(
                name: "busket",
                url: "{lang}/busket",
                defaults: new { controller = "order", action = "busket" },
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "games",
                url: "{lang}/games",
                defaults: new { lang = "en", controller = "Game", action = "games" },
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "orders",
                url: "{lang}/orders",
                defaults: new { lang = "en", controller = "Order", action = "getCurrentOrders" },
                constraints: new { lang = LangConstraint });


            routes.MapRoute(
                name: "gameKey",
                url: "{lang}/game/{key}",
                defaults: new { lang = "en", controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional },
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "publisherDetails",
                url: "{lang}/publisher/{action}/{CompanyName}",
                defaults: new { lang = "en", controller = "publisher", CompanyName = UrlParameter.Optional },
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "gameComments",
                url: "{lang}/game/{gamekey}/comments",
                defaults: new { controller = "comment", action="comments"},
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "gameKeyAction",
                url: "{lang}/game/{gamekey}/{action}",
                defaults: new { lang = "en", controller = "game" },
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "defaultS",
                url: "{lang}/{controller}s/{action}/{key}",
                defaults: new { lang = "en", controller = "Game", action = "Games", key = UrlParameter.Optional },
                constraints: new { lang = LangConstraint });

            routes.MapRoute(
                name: "default",
                url: "{lang}/{controller}/{action}/{id}/{gamekey}",
                defaults: new { lang = "en", controller = "Game", action = "Games", id = UrlParameter.Optional, gamekey = UrlParameter.Optional },
                constraints: new { lang = LangConstraint });
            #endregion

            #region DefaultRoutes
            routes.MapRoute(
                name: "Lbusket",
                url: "busket",
                defaults: new { lang = "en", controller = "order", action = "busket" }
            );

            routes.MapRoute(
                name: "Lgames",
                url: "games",
                defaults: new { lang = "en", controller = "Game", action = "games" }
            );

            routes.MapRoute(
                name: "Lorders",
                url: "orders",
                defaults: new { lang = "en", controller = "Order", action = "getCurrentOrders" }
            );

            routes.MapRoute(
               name: "LgameKey",
               url: "game/{key}",
               defaults: new { lang = "en", controller = "Game", action = "GetGameDetails", key = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "LpublisherDetails",
                url: "publisher/{action}/{CompanyName}",
                defaults: new { lang = "en", controller = "publisher", CompanyName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LgameComments",
                url: "game/{gamekey}/comments",
                defaults: new { lang = "en", controller = "comment", action = "comments" });

            routes.MapRoute(
                 name: "LgameKeyAction",
                 url: "game/{gamekey}/{action}",
                 defaults: new { lang = "en", controller = "game" }
             );

            routes.MapRoute(
                name: "LdefaultS",
                url: "{controller}s/{action}/{key}",
                defaults: new { lang = "en", controller = "Game", action = "Games", key = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Ldefault",
                url: "{controller}/{action}/{id}",
                defaults: new { lang = "en", controller = "Game", action = "Games", id = UrlParameter.Optional, gamekey = UrlParameter.Optional }
            );
            #endregion
        }
    }
}
