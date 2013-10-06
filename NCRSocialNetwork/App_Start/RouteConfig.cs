using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NCRSocialNetwork
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Profile",
                url: "Profile/{userId}",
                defaults: new { controller = "Profile", action = "Details"}
            );

            routes.MapRoute(
                name: "Clubs",
                url: "Clubs/{clubId}/{userId}",
                defaults: new { controller = "Club", action = "ClubPage", clubId = UrlParameter.Optional, userId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}