using System.Web.Http;

namespace Granta.MaterialsWall
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "search/{term}",
                defaults: new { controller = "Search", action = "Get", term = RouteParameter.Optional }
            );
        }
    }
}
