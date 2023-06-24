using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Post
        public ActionResult Index()
        {
            var items = db.Posts.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CategoryID = 1;
                model.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);
                db.Posts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Posts.Where(p => p.Id == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {
                var news = db.Posts.FirstOrDefault(c => c.Id == model.Id);
                if (news != null)
                {
                    news.ModifiedDate = DateTime.Now;
                    news.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);
                    news.Title = model.Title;
                    news.Detail = model.Detail;
                    news.IsActive = model.IsActive;
                    news.Image= model.Image;
                    news.Description = model.Description;
                    news.SeoKeywords = model.SeoKeywords;
                    news.SeoDescription = model.SeoDescription;
                    news.SeoTitle = model.SeoTitle;
                    news.ModifiedBy = model.ModifiedBy;
                }
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Posts.Find(id);
            if (item != null)
            {
                db.Posts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (ids != "")
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Posts.Find(Convert.ToInt32(item));
                        db.Posts.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}