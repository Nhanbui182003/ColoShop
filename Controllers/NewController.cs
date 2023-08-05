using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Controllers
{
    public class NewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: New
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<New> items = db.News.OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue? page.Value : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Detail(string Alias,int Id) 
        {
            var item = db.News.Find(Id);    
            if (item != null) 
            {
                return View(item);
            }
            return null;
        }
        public ActionResult Partial_New_Home()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);
        }

    }
}