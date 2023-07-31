using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Refresh()
        {
            var item = new StatisticModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            item.ThisDay = HttpContext.Application["ThisDay"].ToString() ;
            item.Yesterday = HttpContext.Application["Yesterday"].ToString();
            item.ThisWeek = HttpContext.Application["ThisWeek"].ToString() ;
            item.LastWeek = HttpContext.Application["LastWeek"].ToString() ;
            item.ThisMonth = HttpContext.Application["ThisMonth"].ToString() ;
            item.LastMonth = HttpContext.Application["LastMonth"].ToString() ;
            item.SinceFirstDay = HttpContext.Application["SinceFirstDay"].ToString() ;
            return PartialView(item);
        }
        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop",items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
        public ActionResult Partial_Subcribe()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Subcribe(Subscribe req)
        {
            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe {Email=req.Email ,CreatedDate=DateTime.Now});
                db.SaveChanges();
            }
            return View("Partial_Subcribe",req);
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}