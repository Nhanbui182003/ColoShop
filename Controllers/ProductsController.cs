using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;

namespace WeBanHang.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(int? id)
        {
            var items = db.Products.ToList();
            if (id != null)
            {
                items = items.Where(x=>x.ProductCategoryID== id).ToList();
            }
            return View(items);
        }

        public ActionResult ProductsByProductCateId(string alias, int? id)
        {
            var items = db.Products.ToList();
            if (id != null)
            {
                items = items.Where(x => x.ProductCategoryID == id).ToList();
            }
            var cate = db.ProductCategories.FirstOrDefault(x => x.Id== id);
            if (cate!=null)
            {
                ViewBag.ProductCategoryName = cate.Title;
            }
            ViewBag.ProductCategoryId = id;
            return View(items);
        }

        public ActionResult Partial_ItemByCategoryId()
        {
            var items = db.Products.Where(x=>x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive). Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.ProductCategoryId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }
         
    }
}