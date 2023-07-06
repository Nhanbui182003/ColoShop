using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.ProductCategories;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.CreatedDate= DateTime.Now;
                productCategory.ModifiedDate= DateTime.Now;
                productCategory.Alias = WeBanHang.Models.Commons.Filter.FilterChar(productCategory.Title);
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductCategories.FirstOrDefault(c => c.Id == id);
            if (item != null) 
            {
                db.ProductCategories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}