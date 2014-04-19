using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace zestork
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("Account",
                "Account/{action}/{id}",
                new { controller = "Account", id = UrlParameter.Optional }

                );

            routes.MapRoute("Auth",
                "Auth/{action}/{id}",
                new { controller = "Auth", id = UrlParameter.Optional }

                );

            routes.MapRoute("Image",
                "Image/{action}/{id}",
                new { controller = "Image", action = "Index", id = UrlParameter.Optional }

                );

            routes.MapRoute("secure",
                "secure/{action}/{id}",
                new { controller = "secure", action = "Index", id = UrlParameter.Optional }

                );

            routes.MapRoute("Locked",
                "Locked/{action}/{id}",
                new { controller = "Locked", action = "Index", id = UrlParameter.Optional }

                );

            routes.MapRoute("Client",
                "Client/{action}/{id}",
                new { controller = "Client", action = "Index", id = UrlParameter.Optional }

                );

            routes.MapRoute("UserDetails",
                "{id}/{controller}/{action}",
                new { controller = "User", action = "index" }

                );
            
        }
    }
}