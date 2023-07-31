using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI;
using WeBanHang.Models;
using WeBanHang.Models.EF;
using WebGrease;

namespace WeBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class ProductsController : Controller
    {
        // GET: Admin/Product
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchText,int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Contains(searchText) || x.Title.Contains(searchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(),"Id","Title");
            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault) 
        { 
            if (ModelState.IsValid)
            {
                if (Images!=null && Images.Count>0)
                {
                    for (int i=0;i<Images.Count;i++) 
                    { 
                        if (i + 1 == rDefault[0])
                        {
                            model.ProductImages.Add(new ProductImage {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                            });
                        }
                        else
                        {
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle= model.Title;
                }
                    if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);

                }
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            var item = db.Products.Include(x=>x.ProductImages).FirstOrDefault(x=>x.Id==id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == model.Id);
                if (existingProduct != null)
                {
                    // Remove existing product images
                    db.ProductImages.RemoveRange(existingProduct.ProductImages);
                }
                db.SaveChanges();
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        bool isDefault = (i + 1 == rDefault[0]);
                        var productImage = new ProductImage
                        {
                            ProductId = model.Id,
                            Image = Images[i],
                            IsDefault = isDefault,
                        };
                        model.ProductImages.Add(productImage);
                    }
                }

                model.ModifiedDate = DateTime.Now;
                model.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);

                db.Set<Product>().AddOrUpdate(model);
                foreach (var productImage in model.ProductImages)
                {
                    db.Entry(productImage).State = EntityState.Added;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true});

            }
            return Json(new { success = false});
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
                return Json(new {success = true, isActive= item.IsActive});
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (ids != "")
            {
                var items = ids.Split(',');
                if (items!=null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Products.Find(Convert.ToInt32(item));
                        db.Products.Remove(obj); 
                        db.SaveChanges();
                    }
                    return Json(new { success = true });

                }

            }
            return Json(new { success = false });
        }

    }
}