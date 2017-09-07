using System.Security.Policy;
using System.Web.Http;

namespace GameStore.Web
{
    public class WebApiRouteConfig
    {
        private const string LanguageConstraint = "en|ru";
        private const string ContentTypeConstraint = "json|xml";
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "Get",
                "api/{lang}/{controller}/{contentType}",
                new { contentType = "json" },
                new { lang = LanguageConstraint, contentType = ContentTypeConstraint }
            );

            config.Routes.MapHttpRoute(
                "GetPostPutDelete",
                "api/{lang}/{controller}/{key}/{contentType}",
                new { contentType = "json" },
                new { lang = LanguageConstraint, contentType = ContentTypeConstraint }
            );

            config.Routes.MapHttpRoute(
                "CommentsGenresPpublishersOfGame",
                "api/{lang}/games/{key}/{controller}/{contentType}",
                new { contentType = "json", action = "GetAllByGameKey" },
                new { lang = LanguageConstraint, contentType = ContentTypeConstraint, controller = "comments|genres|publishers" }
            );

            config.Routes.MapHttpRoute(
                "Comments",
                "api/{lang}/games/{key}/comments/{id}/{contentType}",
                new { contentType = "json", controller = "comments" },
                new { lang = LanguageConstraint, contentType = ContentTypeConstraint }
            );

            config.Routes.MapHttpRoute(
                "GamesOfPublisher",
                "api/{lang}/publishers/{key}/games/{contentType}",
                new { contentType = "json", action = "GetAllByCompanyName", controller = "games" },
                new { lang = LanguageConstraint, contentType = ContentTypeConstraint }
            );

            config.Routes.MapHttpRoute(
                "GamesOfGenre",
                "api/{lang}/genres/{key}/games/{contentType}",
                new { contentType = "json", action = "GetAllByGenreName", controller = "games" },
                new { lang = LanguageConstraint, contentType = ContentTypeConstraint }
            );


        }
    }
}