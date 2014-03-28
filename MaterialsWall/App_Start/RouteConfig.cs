﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Granta.MaterialsWall
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Search",
                url: "search/{term}",
                defaults: new { controller = "Search", action = "Index", term = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{identifier}",
                defaults: new { controller = "Home", action = "Index", identifier = UrlParameter.Optional },
                constraints: new { controller = "(?!qr).*" }
            );
            routes.MapRoute(
                name: "QRCodes",
                url: "QR/{identifier}",
                defaults: new { controller = "Card", action = "Index", identifier = UrlParameter.Optional }
            );
        }
    }
}
