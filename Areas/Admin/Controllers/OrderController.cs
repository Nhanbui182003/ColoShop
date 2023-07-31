using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(string searchText,int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Order> items = db.Orders.OrderByDescending(x=>x.CreatedDate).ToList();
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x=>x.Code.Contains(searchText));
            }
            var pageIndex = page.HasValue ? page.Value : 1;
            items = items.ToPagedList(pageIndex,pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult View(int id)
        {
            var item = db.Orders.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return View(item);
            }
            return null;
        }
        public ActionResult Partial_ListProduct(int id)
        {
            var items = db.OrderDetails.Where(x=>x.OrderId==id).ToList();
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult UpdatePayment(int id)
        {
            var order = db.Orders.FirstOrDefault(x=>x.Id==id);
            if (order != null)
            {
                order.Payment = 2;
                db.SaveChanges();
                return Json(new {success=true});
            }
            return Json(new {success=false});   
        }
    }

}