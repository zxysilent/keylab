using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Keylab.Web {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
              name: "List",
              url: "{controller}/{action}/{super}/{suber}",
              defaults: new { controller = "List", action = "Index" },
              constraints: new { controller = @"^list.*|^List.*" },
              namespaces: new string[] { "Keylab.Web.Controllers" }
           );
            routes.MapRoute(
                name: "Detail",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Detail", action = "Index" },
                constraints: new { id = @"\d+", controller = @"^detail.*|^Detail.*" },
                namespaces: new string[] { "Keylab.Web.Controllers" }
            );
            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                 constraints: new { controller = @"^[^d|^l].*" },
                 namespaces: new string[] { "Keylab.Web.Controllers" }
             );
        }
    }
}