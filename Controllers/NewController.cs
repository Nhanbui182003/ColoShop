using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;

namespace WeBanHang.Controllers
{
    public class NewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: New
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_New_Home()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);
        }

    }
}