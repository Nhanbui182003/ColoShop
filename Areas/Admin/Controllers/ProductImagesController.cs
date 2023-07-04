using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;

namespace WeBanHang.Areas.Admin.Controllers
{
    public class ProductImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImages
        public ActionResult Index(int id)
        {
            var items = db.ProductImages.Where(p=>p.ProductId==id).ToList();
            return View(items);
        }
    }
}