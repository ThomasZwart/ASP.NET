using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


           // routes.MapMvcAttributeRoutes();

            // Eigen route maken
            // Specifiekere route moet boven en meer generic onder
            // Convention based routing (The old way)
            /*routes.MapRoute(
                "Movies",
                "movie/released/{year}/{month}",
                // default
                new { controller = "Movie", action = "ByReleaseDate" },
                // Regular expressions for constraints on what can be in de URL
                new { year = @"\d{4}", month = @"\d{2}" });
            */

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
