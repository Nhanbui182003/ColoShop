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
                defaults: new { controller = "Products", action = "Index" },
                namespaces: new[] { "WeBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "ShoppingCart",
                url: "gio-hang",
                defaults: new { controller = "ShoppingCart", action = "Index" },
                namespaces: new[] { "WeBanHang.Controllers" }
            );

            routes.MapRoute(
               name: "DetailProduct",
               url: "chi-tiet/{alias}-{id}",
               defaults: new { controller = "Products", action = "Detail" },
               namespaces: new[] { "WeBanHang.Controllers" }
           );



            routes.MapRoute(
                name: "Products of category",
                url: "danh-muc-san-pham-{alias}-{id}",
                defaults: new { controller = "Products", action = "ProductsByProductCateId" },
                namespaces: new[] { "WeBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "Checkout",
                url: "thanh-toan",
                defaults: new { controller = "ShoppingCart", action = "Checkout" },
                namespaces: new[] { "WeBanHang.Controllers" }
            );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", },
               namespaces: new[] { "WeBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "NewList",
               url: "tin-tuc",
               defaults: new { controller = "New", action = "Index" },
               namespaces: new[] { "WeBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "NewDetail",
               url: "chi-tiet-tin-tuc/{Alias}-{Id}",
               defaults: new { controller = "New", action = "Detail" },
               namespaces: new[] { "WeBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "PostDetail",
               url: "bai-viet/{Alias}",
               defaults: new { controller = "Post", action = "Index" },
               namespaces: new[] { "WeBanHang.Controllers" }
           );

            routes.MapRoute(
                name: "HomePage",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index"},
                namespaces: new[] { "WeBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "AdminRedirect",
                url: "admin",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "WeBanHang.Areas.Admin.Controllers" }

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
