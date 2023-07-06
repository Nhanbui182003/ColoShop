using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WeBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CategoryProduct",
                url: "san-pham",
                defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WeBanHang.Controllers" }
            );

            routes.MapRoute(
               name: "ProductsByProductCateId",
               url: "{alias}-{id}",
               defaults: new { controller = "Products", action = "ProductsByProductCateId", id = UrlParameter.Optional, alias = UrlParameter.Optional },
               namespaces: new[] { "WeBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "WeBanHang.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces : new [] { "WeBanHang.Controllers" }
            );
        }
    }
}
